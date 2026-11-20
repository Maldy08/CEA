using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Application.UseCases.Nomina.Queries.GetConsultaVacaciones;
using CEA.Domain.Entities.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Commands.CreateSolicitudVacaciones
{
    // Paso 1 de la pestaña "Procesos" (Generación del trámite de vacaciones): INSERT en NOM_TRAMITES,
    // NOM_TRAMITES_BITACORA, NOM_VACAEMP_M y NOM_VACAEMP_D con estatus 1. Enviarlo al jefe (2) es otro paso.
    // Días y reparto por periodo se recalculan aquí con FN_CONSULTA_VACACIONES; no se confía en lo que mande el front.
    public record CreateSolicitudVacacionesCommand : IRequest<Result<SolicitudVacacionesCreadaDto>>
    {
        // Beneficiario.
        public int NoEmp { get; init; }
        // Quien captura (usuario de la sesión). Si es distinto del beneficiario, el trámite va con ámbito 'S'.
        public int NoEmpCaptura { get; init; }
        public DateTime Desde { get; init; }
        public DateTime Hasta { get; init; }
        // La captura la sugiere y el usuario puede cambiarla; nula = siguiente día hábil después de Hasta.
        public DateTime? FechaRegreso { get; init; }
        public int? IdEmpleadoCubre { get; init; }
        public string? Comentarios { get; init; }
    }

    internal class CreateSolicitudVacacionesCommandHandler
        : IRequestHandler<CreateSolicitudVacacionesCommand, Result<SolicitudVacacionesCreadaDto>>
    {
        private const int TipoVacaciones = 1;
        private const int EstatusCreado = 1;
        private const string AccionGenerada = "SOLICITUD GENERADA";

        private readonly IMediator _mediator;
        private readonly INomTramitesRepository _tramites;
        private readonly INomDiasFestivosRepository _festivos;
        private readonly INomTramitesFunctions _funciones;
        private readonly INomVacacionesFunctions _vacaciones;

        public CreateSolicitudVacacionesCommandHandler(IMediator mediator, INomTramitesRepository tramites,
            INomDiasFestivosRepository festivos, INomTramitesFunctions funciones, INomVacacionesFunctions vacaciones)
        {
            _funciones = funciones;
            _vacaciones = vacaciones;
            _mediator = mediator;
            _tramites = tramites;
            _festivos = festivos;
        }

        public async Task<Result<SolicitudVacacionesCreadaDto>> Handle(CreateSolicitudVacacionesCommand request,
            CancellationToken cancellationToken)
        {
            var desde = request.Desde.Date;
            var hasta = request.Hasta.Date;
            if (hasta < desde)
                return await Fallo("La fecha final no puede ser anterior a la inicial.");
            if (request.FechaRegreso.HasValue && request.FechaRegreso.Value.Date <= hasta)
                return await Fallo("La fecha de regreso debe ser posterior a la fecha final.");
            if (request.IdEmpleadoCubre == request.NoEmp)
                return await Fallo("El empleado no puede cubrirse a sí mismo.");
            var comentarios = string.IsNullOrWhiteSpace(request.Comentarios) ? null : request.Comentarios.Trim();
            if (comentarios?.Length > 1000)
                return await Fallo("Los comentarios no pueden pasar de 1000 caracteres.");

            var traslape = await _tramites.GetFolioVacacionesTraslapadoAsync(request.NoEmp, desde, hasta);
            if (traslape != null)
                return await Fallo($"Las fechas se cruzan con la solicitud {traslape}.");

            var consulta = await _mediator.Send(new GetConsultaVacacionesQuery(request.NoEmp, desde, hasta), cancellationToken);
            if (!consulta.Succeeded)
                return await Result<SolicitudVacacionesCreadaDto>.FailureAsync(consulta.Messages);
            var calculo = consulta.Data;
            if (calculo.DiasHabiles <= 0)
                return await Fallo("El rango no tiene días hábiles.");
            if (calculo.Periodos.Sum(p => p.Dias) < calculo.DiasHabiles)
                return await Fallo($"No tiene saldo suficiente para {calculo.DiasHabiles} días.");
            // Candado contra el saldo OFICIAL (F_SALDO_VACACIONES, el que ve el empleado en la captura): F_SIMULA reparte
            // por año calendario y puede usar periodos que el empleado aún no gana por antigüedad (p. ej. 7139: saldo 34,
            // F_SIMULA cubre 60 con los periodos 2026).
            var saldo = await _vacaciones.SaldoVacaciones(request.NoEmp);
            var saldoTotal = saldo?.DiasTotalesPorDisfrutar ?? 0;
            if (calculo.DiasHabiles > saldoTotal)
                return await Fallo($"Saldo insuficiente: solicita {calculo.DiasHabiles} días hábiles y su saldo de vacaciones es de {saldoTotal:0.#} días.");
            // NOM_VACAEMP_D.DIASVAC/DIASXDISF son enteros; F_SIMULA puede dar medios días (DIAS/2).
            if (calculo.Periodos.Any(p => p.Dias != decimal.Truncate(p.Dias) || p.DiasXDisfrutar != decimal.Truncate(p.DiasXDisfrutar)))
                return await Fallo("El reparto por periodo trae medios días; no se puede guardar. Avise a Recursos Humanos.");

            // Quiénes firman, según el depto del empleado. Sin firmas no se puede seguir el flujo.
            var firmas = await _funciones.ConsultaFirmas(request.NoEmp);
            if (firmas == null || (firmas.Firma1 == null && firmas.Firma2 == null))
                return await Fallo("No hay firmas configuradas para el departamento del empleado. Avise a Recursos Humanos.");

            var regreso = request.FechaRegreso?.Date ?? await FechaRegresoAsync(_festivos, request.NoEmp, hasta);
            var ahora = DateTime.Now;
            var tramite = new NomTramite
            {
                Ano = ahora.Year,
                TipoTramite = TipoVacaciones,
                IdEmpleado = request.NoEmp,
                Ambito = request.NoEmpCaptura == request.NoEmp ? "P" : "S",
                IdEmpleadoSupervisor = request.NoEmpCaptura == request.NoEmp ? null : request.NoEmpCaptura,
                FechaSolicitud = ahora,
                Estatus = EstatusCreado,
                Valida1 = firmas.Firma1,
                Valida2 = firmas.Firma2
            };
            var bitacora = new NomTramiteBitacora
            {
                FechaMovimiento = ahora,
                IdEmpleado = request.NoEmpCaptura,
                IdEstatus = EstatusCreado,
                Accion = AccionGenerada,
                Comentarios = comentarios
            };
            var solicitud = new NomVacaempM
            {
                NoEmp = request.NoEmp,
                DiasSolicitados = calculo.DiasHabiles,
                FDesde = desde,
                FHasta = hasta,
                FechaRegreso = regreso,
                IdEmpleadoCubre = request.IdEmpleadoCubre,
                OrigenRegistro = NomVacaempM.OrigenTramite,
                IdEmpleadoCaptura = request.NoEmpCaptura,
                FechaCaptura = ahora
            };
            var periodos = calculo.Periodos
                .Where(p => p.Dias > 0)
                .Select(p => new NomVacaempD
                {
                    NoEmp = request.NoEmp,
                    Ano = p.Ano,
                    PerVac = $"{p.Ano}-{p.Periodo}",
                    DiasVac = (int)p.Dias,
                    DiasXDisf = (int)p.DiasXDisfrutar
                })
                .ToList();

            var (creado, m) = await _tramites.CreateSolicitudVacacionesAsync(tramite, bitacora, solicitud, periodos, cancellationToken);
            return await Result<SolicitudVacacionesCreadaDto>.SuccessAsync(new SolicitudVacacionesCreadaDto
            {
                IdTramite = creado.IdTramite,
                IdVacaemp = m.IdVacaemp,
                Folio = creado.Folio,
                FechaRegreso = regreso
            }, $"Solicitud generada con folio {creado.Folio}");
        }

        // Siguiente día hábil después de `hasta`: sin sábados, domingos ni festivos de día completo del empleado.
        // También la usa la modificación de RH (ModificarVacacionesRh).
        internal static async Task<DateTime> FechaRegresoAsync(INomDiasFestivosRepository repo, int noEmp, DateTime hasta)
        {
            var festivos = (await repo.GetByEmpleadoAsync(noEmp, hasta.AddDays(1), hasta.AddDays(31)))
                .Where(f => f.HoraEntrada == null && f.HoraSalida == null)
                .Select(f => f.FechaFestivo.Date)
                .ToHashSet();
            var dia = hasta.AddDays(1);
            while (dia.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday || festivos.Contains(dia))
                dia = dia.AddDays(1);
            return dia;
        }

        private static Task<Result<SolicitudVacacionesCreadaDto>> Fallo(string mensaje) =>
            Result<SolicitudVacacionesCreadaDto>.FailureAsync(mensaje);
    }
}
