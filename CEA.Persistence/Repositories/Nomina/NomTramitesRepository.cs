using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Domain.Entities.Nomina;
using CEA.Persistence.Context;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CEA.Persistence.Repositories.Nomina
{
    public class NomTramitesRepository : INomTramitesRepository
    {
        private readonly ApplicationDbContext _context;

        public NomTramitesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<NomTramiteDto>> GetByEmpleadoAsync(int noEmp, int ano, int? tipoTramite)
        {
            return await Encabezados(_context.NomTramites
                    .Where(t => t.IdEmpleado == noEmp && t.Ano == ano && (tipoTramite == null || t.TipoTramite == tipoTramite))
                    .OrderByDescending(t => t.FechaSolicitud)
                    .ThenByDescending(t => t.IdTramite))
                .ToListAsync();
        }

        public async Task<NomTramiteDto?> GetByIdAsync(int idTramite)
        {
            var tramite = await Encabezados(_context.NomTramites.Where(t => t.IdTramite == idTramite)).FirstOrDefaultAsync();
            if (tramite == null) return null;

            tramite.Bitacora = await _context.NomTramitesBitacora
                .AsNoTracking()
                .Where(b => b.IdTramite == idTramite)
                .OrderBy(b => b.FechaMovimiento)
                .ThenBy(b => b.IdBitacora)
                .Select(b => new NomTramiteBitacoraDto
                {
                    IdBitacora = b.IdBitacora,
                    FechaMovimiento = b.FechaMovimiento,
                    IdEmpleado = b.IdEmpleado,
                    IdEstatus = b.IdEstatus,
                    Accion = b.Accion,
                    Comentarios = b.Comentarios
                })
                .ToListAsync();
            return tramite;
        }

        public async Task<SolicitudVacacionesDto?> GetSolicitudVacacionesAsync(int idTramite)
        {
            // Una reprogramación borra y vuelve a insertar; si llegara a quedar más de un encabezado, gana el último.
            var solicitud = await _context.NomVacaempM
                .AsNoTracking()
                .Where(m => m.IdTramite == idTramite)
                .OrderByDescending(m => m.IdVacaemp)
                .Select(m => new SolicitudVacacionesDto
                {
                    IdVacaemp = m.IdVacaemp,
                    IdTramite = m.IdTramite,
                    Ano = m.Ano,
                    Folio = m.Folio,
                    NoEmp = m.NoEmp,
                    DiasSolicitados = m.DiasSolicitados,
                    FDesde = m.FDesde,
                    FHasta = m.FHasta,
                    FechaRegreso = m.FechaRegreso,
                    IdEmpleadoCubre = m.IdEmpleadoCubre
                })
                .FirstOrDefaultAsync();
            if (solicitud == null) return null;

            solicitud.Periodos = await _context.NomVacaempD
                .AsNoTracking()
                .Where(d => d.IdVacaemp == solicitud.IdVacaemp)
                .OrderBy(d => d.PerVac)
                .Select(d => new SolicitudVacacionesPeriodoDto
                {
                    PerVac = d.PerVac,
                    Ano = d.Ano,
                    DiasVac = d.DiasVac,
                    DiasXDisf = d.DiasXDisf,
                    NoMov = d.NoMov,
                    FDesde = d.FDesde,
                    FHasta = d.FHasta
                })
                .ToListAsync();
            return solicitud;
        }

        public async Task<string?> GetFolioVacacionesTraslapadoAsync(int noEmp, DateTime desde, DateTime hasta,
            int? excluirIdTramite = null)
        {
            return await (from m in _context.NomVacaempM.AsNoTracking()
                          join t in _context.NomTramites.AsNoTracking() on m.IdTramite equals t.IdTramite
                          where m.NoEmp == noEmp
                                && t.Estatus != EstatusCancelado
                                && (excluirIdTramite == null || t.IdTramite != excluirIdTramite)
                                && m.FDesde <= hasta && m.FHasta >= desde
                          orderby m.FDesde
                          select m.Folio)
                .FirstOrDefaultAsync();
        }

        public async Task<(NomTramite Tramite, NomVacaempM Solicitud)> CreateSolicitudVacacionesAsync(
            NomTramite tramite, NomTramiteBitacora bitacora, NomVacaempM solicitud, List<NomVacaempD> periodos,
            CancellationToken cancellationToken)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);
            await AgregarTramiteAsync(tramite, tx.GetDbTransaction(), cancellationToken);

            bitacora.IdTramite = tramite.IdTramite;
            solicitud.IdTramite = tramite.IdTramite;
            solicitud.Ano = tramite.Ano;
            solicitud.Folio = tramite.Folio;
            _context.NomTramitesBitacora.Add(bitacora);
            _context.NomVacaempM.Add(solicitud);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var periodo in periodos) periodo.IdVacaemp = solicitud.IdVacaemp;
            _context.NomVacaempD.AddRange(periodos);
            await _context.SaveChangesAsync(cancellationToken);

            await tx.CommitAsync(cancellationToken);
            return (tramite, solicitud);
        }

        public async Task<int?> CambiarEstatusAsync(int idTramite, IReadOnlyCollection<int> permitidos, int nuevo,
            NomTramiteBitacora bitacora, bool borrarDetalleVacaciones, CancellationToken cancellationToken)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);

            // Sin componer la consulta: EF la manda tal cual y el FOR UPDATE bloquea el renglón hasta el COMMIT,
            // así dos clics (o dos usuarios) no aplican el mismo cambio dos veces.
            var tramite = (await _context.NomTramites
                    .FromSqlInterpolated($"SELECT * FROM NOM_TRAMITES WHERE ID_TRAMITE = {idTramite} FOR UPDATE")
                    .ToListAsync(cancellationToken))
                .FirstOrDefault();
            if (tramite == null) return null;
            var anterior = tramite.Estatus;
            if (!permitidos.Contains(anterior)) return anterior;

            var ahora = DateTime.Now;
            tramite.Estatus = nuevo;
            if (nuevo == EstatusCancelado) tramite.FechaCancelacion = ahora;
            if (nuevo == EstatusAutorizado) tramite.FechaAutorizacion = ahora;
            bitacora.IdTramite = idTramite;
            bitacora.IdEstatus = nuevo;
            bitacora.FechaMovimiento = ahora;
            _context.NomTramitesBitacora.Add(bitacora);
            await _context.SaveChangesAsync(cancellationToken);

            if (borrarDetalleVacaciones)
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"DELETE FROM NOM_VACAEMP_D WHERE ID_VACAEMP IN (SELECT ID_VACAEMP FROM NOM_VACAEMP_M WHERE ID_TRAMITE = {idTramite})",
                    cancellationToken);
            }

            await tx.CommitAsync(cancellationToken);
            return anterior;
        }

        public async Task<string?> GetFolioPermisoTraslapadoAsync(int noEmp, int tipoTramite, int? tipoIncidencia,
            DateTime desde, DateTime hasta, int? excluirIdTramite = null)
        {
            return await (from p in _context.NomPermisosJustificantes.AsNoTracking()
                          join t in _context.NomTramites.AsNoTracking() on p.IdTramite equals t.IdTramite
                          where t.IdEmpleado == noEmp
                                && t.TipoTramite == tipoTramite
                                && (tipoIncidencia == null || p.TipoIncidencia == tipoIncidencia)
                                && t.Estatus != EstatusCancelado
                                && (excluirIdTramite == null || t.IdTramite != excluirIdTramite)
                                && p.FechaInicio <= hasta && p.FechaFin >= desde
                          orderby p.FechaInicio
                          select t.Folio)
                .FirstOrDefaultAsync();
        }

        public async Task<(NomTramite Tramite, NomPermisoJustificante Detalle)> CreatePermisoJustificanteAsync(
            NomTramite tramite, NomTramiteBitacora bitacora, NomPermisoJustificante detalle, CancellationToken cancellationToken)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);
            await AgregarTramiteAsync(tramite, tx.GetDbTransaction(), cancellationToken);

            bitacora.IdTramite = tramite.IdTramite;
            detalle.IdTramite = tramite.IdTramite;
            _context.NomTramitesBitacora.Add(bitacora);
            _context.NomPermisosJustificantes.Add(detalle);
            await _context.SaveChangesAsync(cancellationToken);

            await tx.CommitAsync(cancellationToken);
            return (tramite, detalle);
        }

        // Inserta el trámite con su folio <SIGLAS>-NNNN-AAAA (consecutivo por tipo y año) dentro de la transacción `tx`.
        // El FOR UPDATE sobre el renglón del catálogo serializa a quienes generan folio del mismo tipo:
        // el segundo espera al COMMIT del primero y ya ve su folio.
        private async Task AgregarTramiteAsync(NomTramite tramite, DbTransaction tx, CancellationToken cancellationToken)
        {
            var siglas = await SiglasBloqueadasAsync(tramite.TipoTramite, tx, cancellationToken);
            var folios = await _context.NomTramites
                .AsNoTracking()
                .Where(t => t.TipoTramite == tramite.TipoTramite && t.Ano == tramite.Ano)
                .Select(t => t.Folio)
                .ToListAsync(cancellationToken);
            tramite.Folio = $"{siglas}-{SiguienteConsecutivo(folios):D4}-{tramite.Ano}";

            _context.NomTramites.Add(tramite);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private const int EstatusCancelado = 99;
        private const int EstatusAutorizado = 100;
        private const int TipoViaticos = 2;

        public async Task<List<TramiteRhDto>> GetTodosAsync(int ano, int? tipoTramite, int? estatus)
        {
            var tramites = _context.NomTramites.AsNoTracking()
                .Where(t => t.Ano == ano && t.TipoTramite != TipoViaticos
                            && (tipoTramite == null || t.TipoTramite == tipoTramite)
                            && (estatus == null || t.Estatus == estatus));

            var lista = await (from t in tramites
                               from m in _context.NomVacaempM.Where(m => m.IdTramite == t.IdTramite).DefaultIfEmpty()
                               from p in _context.NomPermisosJustificantes.Where(p => p.IdTramite == t.IdTramite).DefaultIfEmpty()
                               orderby t.FechaSolicitud descending, t.IdTramite descending
                               select new TramiteRhDto
                               {
                                   IdTramite = t.IdTramite,
                                   Ano = t.Ano,
                                   Folio = t.Folio,
                                   TipoTramite = t.TipoTramite,
                                   IdEmpleado = t.IdEmpleado,
                                   Ambito = t.Ambito,
                                   IdEmpleadoSupervisor = t.IdEmpleadoSupervisor,
                                   FechaSolicitud = t.FechaSolicitud,
                                   Estatus = t.Estatus,
                                   FechaAutorizacion = t.FechaAutorizacion,
                                   FechaCancelacion = t.FechaCancelacion,
                                   Valida1 = t.Valida1,
                                   Valida2 = t.Valida2,
                                   FechaInicio = m != null ? m.FDesde : p != null ? p.FechaInicio : null,
                                   FechaFin = m != null ? m.FHasta : p != null ? p.FechaFin : null,
                                   TipoIncidencia = p != null ? p.TipoIncidencia : null
                               })
                .ToListAsync();

            // VS_EMPLEADOS no tiene llave: se cruza aparte por número de empleado (Oracle limita un IN a 1000).
            var empleados = new Dictionary<int, (string Nombre, string Depto)>();
            foreach (var bloque in lista.Select(t => t.IdEmpleado).Distinct().Chunk(900))
            {
                var encontrados = await _context.Empleado.AsNoTracking()
                    .Where(e => bloque.Contains(e.IdEmpleado))
                    .Select(e => new { e.IdEmpleado, e.NombreCompleto, e.DescripcionDepto })
                    .ToListAsync();
                foreach (var e in encontrados) empleados[e.IdEmpleado] = (e.NombreCompleto, e.DescripcionDepto);
            }
            foreach (var t in lista)
            {
                if (!empleados.TryGetValue(t.IdEmpleado, out var e)) continue;
                t.NombreEmpleado = e.Nombre;
                t.DescripcionDepto = e.Depto;
            }
            return lista;
        }

        public async Task<PermisoJustificanteDto?> GetPermisoJustificanteAsync(int idTramite)
        {
            return await _context.NomPermisosJustificantes.AsNoTracking()
                .Where(p => p.IdTramite == idTramite)
                .Select(p => new PermisoJustificanteDto
                {
                    IdPermisoJustificante = p.IdPermisoJustificante,
                    IdTramite = p.IdTramite,
                    TipoIncidencia = p.TipoIncidencia,
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin,
                    JustificacionMotivo = p.JustificacionMotivo,
                    IdEmpleadoCubre = p.IdEmpleadoCubre
                })
                .FirstOrDefaultAsync();
        }

        public async Task<string?> ModificarVacacionesAsync(int idTramite,
            Func<CancellationToken, Task<(string? Error, NomVacaempM? Datos, List<NomVacaempD> Periodos)>> recalcular,
            NomTramiteBitacora bitacora, CancellationToken cancellationToken)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);
            var tramite = await TramiteBloqueadoAsync(idTramite, cancellationToken);
            if (tramite == null) return "El trámite no existe.";
            if (tramite.Estatus == EstatusCancelado) return "El trámite está cancelado; ya no se puede modificar.";

            var solicitud = await _context.NomVacaempM
                .Where(m => m.IdTramite == idTramite)
                .OrderByDescending(m => m.IdVacaemp)
                .FirstOrDefaultAsync(cancellationToken);
            if (solicitud == null) return "El trámite no tiene solicitud de vacaciones.";

            // Sin sus propios días, el saldo que calcula FN_CONSULTA_VACACIONES (misma transacción) los vuelve a contar.
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE FROM NOM_VACAEMP_D WHERE ID_VACAEMP = {solicitud.IdVacaemp}", cancellationToken);

            var (error, datos, periodos) = await recalcular(cancellationToken);
            if (error != null || datos == null)
            {
                await tx.RollbackAsync(cancellationToken);
                return error ?? "No se pudo recalcular la solicitud.";
            }

            solicitud.DiasSolicitados = datos.DiasSolicitados;
            solicitud.FDesde = datos.FDesde;
            solicitud.FHasta = datos.FHasta;
            solicitud.FechaRegreso = datos.FechaRegreso;
            solicitud.IdEmpleadoCubre = datos.IdEmpleadoCubre;
            foreach (var periodo in periodos) periodo.IdVacaemp = solicitud.IdVacaemp;
            _context.NomVacaempD.AddRange(periodos);
            AgregarBitacora(tramite, bitacora);
            await _context.SaveChangesAsync(cancellationToken);

            await tx.CommitAsync(cancellationToken);
            return null;
        }

        public async Task<string?> ModificarPermisoJustificanteAsync(int idTramite, Action<NomPermisoJustificante> aplicar,
            NomTramiteBitacora bitacora, CancellationToken cancellationToken)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(cancellationToken);
            var tramite = await TramiteBloqueadoAsync(idTramite, cancellationToken);
            if (tramite == null) return "El trámite no existe.";
            if (tramite.Estatus == EstatusCancelado) return "El trámite está cancelado; ya no se puede modificar.";

            var detalle = await _context.NomPermisosJustificantes.FirstOrDefaultAsync(p => p.IdTramite == idTramite, cancellationToken);
            if (detalle == null) return "El trámite no tiene detalle de permiso o justificante.";

            aplicar(detalle);
            AgregarBitacora(tramite, bitacora);
            await _context.SaveChangesAsync(cancellationToken);

            await tx.CommitAsync(cancellationToken);
            return null;
        }

        // Igual que en CambiarEstatusAsync: FromSql sin componer para que el FOR UPDATE llegue tal cual.
        private async Task<NomTramite?> TramiteBloqueadoAsync(int idTramite, CancellationToken cancellationToken) =>
            (await _context.NomTramites
                .FromSqlInterpolated($"SELECT * FROM NOM_TRAMITES WHERE ID_TRAMITE = {idTramite} FOR UPDATE")
                .ToListAsync(cancellationToken))
            .FirstOrDefault();

        // Movimiento sin cambio de estatus: queda el que tiene el trámite.
        private void AgregarBitacora(NomTramite tramite, NomTramiteBitacora bitacora)
        {
            bitacora.IdTramite = tramite.IdTramite;
            bitacora.IdEstatus = tramite.Estatus;
            bitacora.FechaMovimiento = DateTime.Now;
            _context.NomTramitesBitacora.Add(bitacora);
        }

        private async Task<string> SiglasBloqueadasAsync(int tipoTramite, DbTransaction tx, CancellationToken cancellationToken)
        {
            await using var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = "SELECT SIGLAS FROM RH_CAT_TIPOS_TRAMITE WHERE ID_TIPO_TRAMITE = :tipo FOR UPDATE";
            var p = cmd.CreateParameter();
            p.ParameterName = "tipo";
            p.Value = tipoTramite;
            cmd.Parameters.Add(p);
            var siglas = await cmd.ExecuteScalarAsync(cancellationToken) as string;
            if (string.IsNullOrWhiteSpace(siglas))
                throw new InvalidOperationException($"El tipo de trámite {tipoTramite} no tiene SIGLAS en RH_CAT_TIPOS_TRAMITE.");
            return siglas.Trim();
        }

        // Folio "VAC-0015-2026": el consecutivo es el segundo segmento. Ignora los que no tengan ese formato.
        private static int SiguienteConsecutivo(IEnumerable<string> folios)
        {
            var max = 0;
            foreach (var folio in folios)
            {
                var partes = folio.Split('-');
                if (partes.Length == 3 && int.TryParse(partes[1], out var n) && n > max) max = n;
            }
            return max + 1;
        }

        private static IQueryable<NomTramiteDto> Encabezados(IQueryable<NomTramite> tramites) =>
            tramites.AsNoTracking().Select(t => new NomTramiteDto
            {
                IdTramite = t.IdTramite,
                Ano = t.Ano,
                Folio = t.Folio,
                TipoTramite = t.TipoTramite,
                IdEmpleado = t.IdEmpleado,
                Ambito = t.Ambito,
                IdEmpleadoSupervisor = t.IdEmpleadoSupervisor,
                FechaSolicitud = t.FechaSolicitud,
                Estatus = t.Estatus,
                FechaAutorizacion = t.FechaAutorizacion,
                FechaCancelacion = t.FechaCancelacion,
                Valida1 = t.Valida1,
                Valida2 = t.Valida2
            });
    }
}
