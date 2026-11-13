using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.TramitesRh
{
    // Administración de Trámites de RH: solo para USUARIOS.TRAMITES_NIVEL = 9 (INomTramitesFunctions.EsAdministradorRh).
    // NoEmpActor = claim "userid" del token (lo pone el controlador, no el front).

    // Todos los trámites del año en cualquier estatus (sin viáticos).
    public record GetTramitesRhQuery(int NoEmpActor, int Ano, int? TipoTramite, int? Estatus) : IRequest<Result<List<TramiteRhDto>>>;

    // Trámite con bitácora y su detalle (vacaciones o permiso/justificante) para modificarlo.
    public record GetDetalleTramiteRhQuery(int NoEmpActor, int IdTramite) : IRequest<Result<DetalleTramiteRhDto>>;

    internal class GetTramitesRhQueryHandler : IRequestHandler<GetTramitesRhQuery, Result<List<TramiteRhDto>>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;

        public GetTramitesRhQueryHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones)
        {
            _tramites = tramites;
            _funciones = funciones;
        }

        public async Task<Result<List<TramiteRhDto>>> Handle(GetTramitesRhQuery request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsAdministradorRh(request.NoEmpActor))
                return await Result<List<TramiteRhDto>>.FailureAsync(TramitesRhMensajes.SinPermiso);
            return await Result<List<TramiteRhDto>>.SuccessAsync(
                await _tramites.GetTodosAsync(request.Ano, request.TipoTramite, request.Estatus));
        }
    }

    internal class GetDetalleTramiteRhQueryHandler : IRequestHandler<GetDetalleTramiteRhQuery, Result<DetalleTramiteRhDto>>
    {
        private const int TipoVacaciones = 1;

        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;
        private readonly IEmpleadoRepository _empleados;

        public GetDetalleTramiteRhQueryHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones,
            IEmpleadoRepository empleados)
        {
            _tramites = tramites;
            _funciones = funciones;
            _empleados = empleados;
        }

        public async Task<Result<DetalleTramiteRhDto>> Handle(GetDetalleTramiteRhQuery request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsAdministradorRh(request.NoEmpActor))
                return await Result<DetalleTramiteRhDto>.FailureAsync(TramitesRhMensajes.SinPermiso);
            var tramite = await _tramites.GetByIdAsync(request.IdTramite);
            if (tramite == null) return await Result<DetalleTramiteRhDto>.FailureAsync("El trámite no existe.");

            var empleado = await _empleados.GetEmpleadoByIdAsync(tramite.IdEmpleado);
            return await Result<DetalleTramiteRhDto>.SuccessAsync(new DetalleTramiteRhDto
            {
                Tramite = tramite,
                NombreEmpleado = empleado?.NombreCompleto,
                Vacaciones = tramite.TipoTramite == TipoVacaciones ? await _tramites.GetSolicitudVacacionesAsync(request.IdTramite) : null,
                PermisoJustificante = tramite.TipoTramite != TipoVacaciones ? await _tramites.GetPermisoJustificanteAsync(request.IdTramite) : null
            });
        }
    }

    internal static class TramitesRhMensajes
    {
        public const string SinPermiso = "Solo el administrador de Recursos Humanos puede hacer esta operación.";
    }
}
