using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Domain.Entities.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Firmas
{
    // Bandeja de firmas (portal next-tramites-cea, /firmas): jefes y directores (USUARIOS.TRAMITES_NIVEL = 3)
    // firman en orden los trámites donde aparecen (pestaña "Procesos"):
    //   2 Revisión Jefe     + VALIDA_1 = él → firma → 3 Revisión Director
    //   3 Revisión Director + VALIDA_2 = él → firma → 4 Revisión RH (RH autoriza al final: 4 → 100)
    // En su turno también puede regresarlo al empleado (98, motivo obligatorio); el empleado lo corrige y lo vuelve a
    // enviar (98 → 2), así que vuelve a firmar el jefe. Si VALIDA_1 = VALIDA_2 firma UNA sola vez: 2 → 4 directo.
    // NoEmpActor = claim "userid" del token (lo pone el controlador, no el front). Sin viáticos.

    public record GetTramitesPorFirmarQuery(int NoEmpActor) : IRequest<Result<List<TramiteRhDto>>>;

    public record FirmarTramiteCommand : IRequest<Result<int>>
    {
        public int IdTramite { get; init; }
        public int NoEmpActor { get; init; }
        public string? Comentarios { get; init; }
    }

    public record RegresarTramiteCommand : IRequest<Result<int>>
    {
        public int IdTramite { get; init; }
        public int NoEmpActor { get; init; }
        public string Comentarios { get; init; } = string.Empty;
    }

    internal static class ReglasFirma
    {
        public const int TipoViaticos = 2;
        public const int RevisionJefe = 2;
        public const int RevisionDirector = 3;
        public const int RevisionRh = 4;
        public const int Regresado = 98;
        public const string SinPermiso = "Solo los jefes y directores que firman trámites pueden hacer esta operación.";
        public const string NoLeToca = "El trámite no existe o no está esperando su firma.";

        // Paso que le toca al actor: "JEFE" (2, VALIDA_1) o "DIRECTOR" (3, VALIDA_2); nulo si no le toca.
        public static string? PasoDe(NomTramiteDto t, int noEmp) =>
            t.TipoTramite == TipoViaticos ? null
            : t.Estatus == RevisionJefe && t.Valida1 == noEmp ? "JEFE"
            : t.Estatus == RevisionDirector && t.Valida2 == noEmp ? "DIRECTOR"
            : null;

        public static string? Limpio(string? texto) => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
    }

    internal class GetTramitesPorFirmarQueryHandler : IRequestHandler<GetTramitesPorFirmarQuery, Result<List<TramiteRhDto>>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;

        public GetTramitesPorFirmarQueryHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones)
        {
            _tramites = tramites;
            _funciones = funciones;
        }

        public async Task<Result<List<TramiteRhDto>>> Handle(GetTramitesPorFirmarQuery request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsFirmante(request.NoEmpActor))
                return await Result<List<TramiteRhDto>>.FailureAsync(ReglasFirma.SinPermiso);
            return await Result<List<TramiteRhDto>>.SuccessAsync(await _tramites.GetPorFirmarAsync(request.NoEmpActor));
        }
    }

    internal class FirmarTramiteCommandHandler : IRequestHandler<FirmarTramiteCommand, Result<int>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;

        public FirmarTramiteCommandHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones)
        {
            _tramites = tramites;
            _funciones = funciones;
        }

        public async Task<Result<int>> Handle(FirmarTramiteCommand request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsFirmante(request.NoEmpActor))
                return await Result<int>.FailureAsync(ReglasFirma.SinPermiso);
            if ((request.Comentarios?.Trim().Length ?? 0) > 1000)
                return await Result<int>.FailureAsync("Los comentarios no pueden pasar de 1000 caracteres.");

            var tramite = await _tramites.GetByIdAsync(request.IdTramite);
            var paso = tramite == null ? null : ReglasFirma.PasoDe(tramite, request.NoEmpActor);
            if (tramite == null || paso == null) return await Result<int>.FailureAsync(ReglasFirma.NoLeToca);

            // Jefe y director son la misma persona: una sola firma cubre los dos pasos.
            var ambos = paso == "JEFE" && tramite.Valida2 == request.NoEmpActor;
            var nuevo = paso == "JEFE" && !ambos ? ReglasFirma.RevisionDirector : ReglasFirma.RevisionRh;
            // Permitido = solo el estatus que se revisó: si otro lo movió mientras tanto, no se aplica (FOR UPDATE).
            var anterior = await _tramites.CambiarEstatusAsync(request.IdTramite, new[] { tramite.Estatus }, nuevo,
                new NomTramiteBitacora
                {
                    IdEmpleado = request.NoEmpActor,
                    Accion = ambos ? "VALIDADO POR EL JEFE Y DIRECTOR" : $"VALIDADO POR EL {paso}",
                    Comentarios = ReglasFirma.Limpio(request.Comentarios)
                },
                borrarDetalleVacaciones: false, cancellationToken);
            if (anterior != tramite.Estatus) return await Result<int>.FailureAsync(ReglasFirma.NoLeToca);
            return await Result<int>.SuccessAsync(nuevo,
                nuevo == ReglasFirma.RevisionDirector ? "Trámite firmado; pasa a firma del director." : "Trámite firmado; pasa a Recursos Humanos.");
        }
    }

    internal class RegresarTramiteCommandHandler : IRequestHandler<RegresarTramiteCommand, Result<int>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;

        public RegresarTramiteCommandHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones)
        {
            _tramites = tramites;
            _funciones = funciones;
        }

        public async Task<Result<int>> Handle(RegresarTramiteCommand request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsFirmante(request.NoEmpActor))
                return await Result<int>.FailureAsync(ReglasFirma.SinPermiso);
            var motivo = ReglasFirma.Limpio(request.Comentarios);
            if (motivo == null) return await Result<int>.FailureAsync("Escriba el motivo por el que regresa el trámite.");
            if (motivo.Length > 1000) return await Result<int>.FailureAsync("El motivo no puede pasar de 1000 caracteres.");

            var tramite = await _tramites.GetByIdAsync(request.IdTramite);
            var paso = tramite == null ? null : ReglasFirma.PasoDe(tramite, request.NoEmpActor);
            if (tramite == null || paso == null) return await Result<int>.FailureAsync(ReglasFirma.NoLeToca);

            // Los días de vacaciones (NOM_VACAEMP_D) se conservan: el empleado corrige y lo vuelve a enviar.
            var anterior = await _tramites.CambiarEstatusAsync(request.IdTramite, new[] { tramite.Estatus }, ReglasFirma.Regresado,
                new NomTramiteBitacora { IdEmpleado = request.NoEmpActor, Accion = $"REGRESADO POR EL {paso}", Comentarios = motivo },
                borrarDetalleVacaciones: false, cancellationToken);
            if (anterior != tramite.Estatus) return await Result<int>.FailureAsync(ReglasFirma.NoLeToca);
            return await Result<int>.SuccessAsync(ReglasFirma.Regresado, "Trámite regresado al empleado.");
        }
    }
}
