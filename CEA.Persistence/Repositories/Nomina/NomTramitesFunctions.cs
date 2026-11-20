using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CEA.Persistence.Repositories.Nomina
{
    public class NomTramitesFunctions : INomTramitesFunctions
    {
        private readonly ApplicationDbContext _context;

        public NomTramitesFunctions(ApplicationDbContext context)
        {
            _context = context;
        }

        // Quién firma el trámite del empleado según su departamento. Un renglón (FIRMA_1, FIRMA_2).
        public async Task<FirmasTramiteDto?> ConsultaFirmas(int noEmp, int depto)
        {
            var firmas = await _context.FirmasTramiteDtos
                .FromSqlInterpolated($"SELECT * FROM TABLE (FN_TRAMITES_CONSULTA_FIRMAS({noEmp},{depto}))")
                .ToListAsync();
            return firmas.FirstOrDefault();
        }

        public async Task<FirmasTramiteDto?> ConsultaFirmas(int noEmp)
        {
            var firmas = await _context.FirmasTramiteDtos
                .FromSqlInterpolated($"SELECT * FROM TABLE (FN_TRAMITES_CONSULTA_FIRMAS({noEmp}, (SELECT DEPTOCOMI FROM EMPLEADOS WHERE EMPLEADO = {noEmp})))")
                .ToListAsync();
            return firmas.FirstOrDefault();
        }

        public async Task<FormatoTramiteDto?> Formato(int idTramite)
        {
            var encabezado = await _context.FormatoTramiteEncabezadoDtos.AsNoTracking()
                .FirstOrDefaultAsync(e => e.IdTramite == idTramite);
            if (encabezado == null) return null;

            var detalle = await _context.FormatoTramiteDetalleDtos.AsNoTracking()
                .Where(d => d.IdTramite == idTramite)
                .OrderBy(d => d.Orden)
                .ToListAsync();
            return new FormatoTramiteDto { Encabezado = encabezado, Detalle = detalle };
        }

        public async Task<bool> SupervisaA(int noEmpUsuario, int noEmp)
        {
            // ODP.NET liga por POSICIÓN: un parámetro por cada aparición, en orden.
            const string sql = @"
SELECT COUNT(*) FROM EMPLEADOS E
 WHERE E.EMPLEADO = :noemp
   AND E.SUPERVISOR_ID IS NOT NULL
   AND EXISTS (SELECT 1 FROM USUARIOS U WHERE U.NO_EMPLEADO = :u1 AND U.TRAMITES_NIVEL IN (2, 3))
   AND (E.SUPERVISOR_ID = :u2
        OR E.DEPTOCOMI = (SELECT S.DEPTOCOMI FROM EMPLEADOS S WHERE S.EMPLEADO = :u3))";

            var conexion = _context.Database.GetDbConnection();
            var abrio = conexion.State != ConnectionState.Open;
            if (abrio) await conexion.OpenAsync();
            try
            {
                await using var cmd = conexion.CreateCommand();
                cmd.CommandText = sql;
                foreach (var (nombre, valor) in new[] { ("noemp", noEmp), ("u1", noEmpUsuario), ("u2", noEmpUsuario), ("u3", noEmpUsuario) })
                {
                    var p = cmd.CreateParameter();
                    p.ParameterName = nombre;
                    p.Value = valor;
                    cmd.Parameters.Add(p);
                }
                return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
            }
            finally
            {
                if (abrio) await conexion.CloseAsync();
            }
        }

        public Task<bool> EsAdministradorRh(int noEmp) => TieneNivel(noEmp, 9);

        public Task<bool> EsFirmante(int noEmp) => TieneNivel(noEmp, 3);

        // USUARIOS.TRAMITES_NIVEL es un rol exacto: se compara con =, nunca con >=.
        private async Task<bool> TieneNivel(int noEmp, int nivel)
        {
            var conexion = _context.Database.GetDbConnection();
            var abrio = conexion.State != ConnectionState.Open;
            if (abrio) await conexion.OpenAsync();
            try
            {
                await using var cmd = conexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM USUARIOS WHERE NO_EMPLEADO = :u AND NVL(TRAMITES, 0) = 1 AND TRAMITES_NIVEL = :n";
                foreach (var (nombre, valor) in new[] { ("u", noEmp), ("n", nivel) })
                {
                    var p = cmd.CreateParameter();
                    p.ParameterName = nombre;
                    p.Value = valor;
                    cmd.Parameters.Add(p);
                }
                return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
            }
            finally
            {
                if (abrio) await conexion.CloseAsync();
            }
        }
    }
}
