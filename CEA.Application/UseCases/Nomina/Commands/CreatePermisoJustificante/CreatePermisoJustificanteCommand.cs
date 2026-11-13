using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Domain.Entities.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Commands.CreatePermisoJustificante
{
    // Generación de un permiso (incidencia 1-2 → TIPO_TRAMITE 3, folio PER) o justificante (3-7 → TIPO_TRAMITE 4,
    // folio JUS): INSERT en NOM_TRAMITES, NOM_TRAMITES_BITACORA y NOM_PERMISOS_JUSTIFICANTES con estatus 1.
    // Enviarlo al jefe es el mismo paso que vacaciones (NomTramites/{id}/Enviar).
    public record CreatePermisoJustificanteCommand : IRequest<Result<PermisoJustificanteCreadoDto>>
    {
        // Beneficiario.
        public int NoEmp { get; init; }
        // Quien captura (usuario de la sesión). Si es distinto del beneficiario, el trámite va con ámbito 'S'.
        public int NoEmpCaptura { get; init; }
        public int TipoIncidencia { get; init; }
        // Permisos: solo cuenta el día. Justificantes: llevan la hora a justificar.
        public DateTime FechaInicio { get; init; }
        public DateTime FechaFin { get; init; }
        public string JustificacionMotivo { get; init; } = string.Empty;
        public int? IdEmpleadoCubre { get; init; }
    }

    internal class CreatePermisoJustificanteCommandHandler
        : IRequestHandler<CreatePermisoJustificanteCommand, Result<PermisoJustificanteCreadoDto>>
    {
        private const int EstatusCreado = 1;
        private const string AccionGenerada = "SOLICITUD GENERADA";

        private readonly INomTramitesRepository _tramites;
        private readonly INomDiasFestivosRepository _festivos;
        private readonly INomTramitesFunctions _funciones;

        public CreatePermisoJustificanteCommandHandler(INomTramitesRepository tramites, INomDiasFestivosRepository festivos,
            INomTramitesFunctions funciones)
        {
            _tramites = tramites;
            _festivos = festivos;
            _funciones = funciones;
        }

        public async Task<Result<PermisoJustificanteCreadoDto>> Handle(CreatePermisoJustificanteCommand request,
            CancellationToken cancellationToken)
        {
            var reglas = await ReglasPermisoJustificante.AplicarAsync(_tramites, _festivos, request.NoEmp,
                request.TipoIncidencia, request.FechaInicio, request.FechaFin, request.JustificacionMotivo, request.IdEmpleadoCubre);
            if (reglas.Error != null) return await Fallo(reglas.Error);
            var tipoTramite = ReglasPermisoJustificante.TipoTramite(request.TipoIncidencia);

            // Quiénes firman, según el depto del empleado. Sin firmas no se puede seguir el flujo.
            var firmas = await _funciones.ConsultaFirmas(request.NoEmp);
            if (firmas == null || (firmas.Firma1 == null && firmas.Firma2 == null))
                return await Fallo("No hay firmas configuradas para el departamento del empleado. Avise a Recursos Humanos.");

            var ahora = DateTime.Now;
            var tramite = new NomTramite
            {
                Ano = ahora.Year,
                TipoTramite = tipoTramite,
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
                Accion = AccionGenerada
            };
            var detalle = new NomPermisoJustificante
            {
                TipoIncidencia = request.TipoIncidencia,
                FechaInicio = reglas.Inicio,
                FechaFin = reglas.Fin,
                JustificacionMotivo = reglas.Motivo,
                IdEmpleadoCubre = request.IdEmpleadoCubre
            };

            var (creado, d) = await _tramites.CreatePermisoJustificanteAsync(tramite, bitacora, detalle, cancellationToken);
            return await Result<PermisoJustificanteCreadoDto>.SuccessAsync(new PermisoJustificanteCreadoDto
            {
                IdTramite = creado.IdTramite,
                IdPermisoJustificante = d.IdPermisoJustificante,
                Folio = creado.Folio,
                DiasHabiles = reglas.DiasHabiles
            }, $"Solicitud generada con folio {creado.Folio}");
        }

        private static Task<Result<PermisoJustificanteCreadoDto>> Fallo(string mensaje) =>
            Result<PermisoJustificanteCreadoDto>.FailureAsync(mensaje);
    }
}
