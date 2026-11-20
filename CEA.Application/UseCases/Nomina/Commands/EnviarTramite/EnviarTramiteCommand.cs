using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Domain.Entities.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Commands.EnviarTramite
{
    // Paso 2 de la pestaña "Procesos": envío al jefe para su autorización (UPDATE NOM_TRAMITES + INSERT bitácora).
    // Desde Creado (1) o Regresado (98, ya corregido por el usuario).
    // Quien lo envía no se firma a sí mismo: si es VALIDA_1 y VALIDA_2 pasa directo a Revisión RH (4);
    // si solo es VALIDA_1, directo a Revisión Director (3).
    public record EnviarTramiteCommand : IRequest<Result<int>>
    {
        public int IdTramite { get; init; }
        // Quien lo envía (usuario de la sesión).
        public int NoEmpActor { get; init; }
    }

    internal class EnviarTramiteCommandHandler : IRequestHandler<EnviarTramiteCommand, Result<int>>
    {
        private static readonly int[] Permitidos = { 1, 98 };
        private const int EstatusRevisionJefe = 2;
        private const int EstatusRevisionDirector = 3;
        private const int EstatusRevisionRh = 4;

        private readonly INomTramitesRepository _tramites;

        public EnviarTramiteCommandHandler(INomTramitesRepository tramites)
        {
            _tramites = tramites;
        }

        public async Task<Result<int>> Handle(EnviarTramiteCommand request, CancellationToken cancellationToken)
        {
            var tramite = await _tramites.GetByIdAsync(request.IdTramite);
            if (tramite == null) return await Result<int>.FailureAsync("El trámite no existe.");

            var esJefe = tramite.Valida1 == request.NoEmpActor;
            var esDirector = tramite.Valida2 == request.NoEmpActor;
            var (nuevo, accion, mensaje) = esJefe && esDirector
                ? (EstatusRevisionRh, "ENVIADO A RECURSOS HUMANOS (LO ENVIA EL JEFE Y DIRECTOR)", "Trámite enviado a Recursos Humanos.")
                : esJefe
                    ? (EstatusRevisionDirector, "ENVIADO A FIRMA DEL DIRECTOR (LO ENVIA EL JEFE)", "Trámite enviado a firma del director.")
                    : (EstatusRevisionJefe, "ENVIADO A FIRMA DEL JEFE", "Trámite enviado a firma del jefe.");

            var anterior = await _tramites.CambiarEstatusAsync(request.IdTramite, Permitidos, nuevo,
                new NomTramiteBitacora { IdEmpleado = request.NoEmpActor, Accion = accion },
                borrarDetalleVacaciones: false, cancellationToken);
            if (anterior == null) return await Result<int>.FailureAsync("El trámite no existe.");
            if (!Permitidos.Contains(anterior.Value))
                return await Result<int>.FailureAsync("Solo se puede enviar a firma un trámite Creado o Regresado.");
            return await Result<int>.SuccessAsync(nuevo, mensaje);
        }
    }
}
