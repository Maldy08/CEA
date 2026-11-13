using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Domain.Entities.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Commands.EnviarTramite
{
    // Paso 2 de la pestaña "Procesos": envío al jefe para su autorización (UPDATE NOM_TRAMITES + INSERT bitácora).
    // Desde Creado (1) o Regresado (98, ya corregido por el usuario).
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

        private readonly INomTramitesRepository _tramites;

        public EnviarTramiteCommandHandler(INomTramitesRepository tramites)
        {
            _tramites = tramites;
        }

        public async Task<Result<int>> Handle(EnviarTramiteCommand request, CancellationToken cancellationToken)
        {
            var anterior = await _tramites.CambiarEstatusAsync(request.IdTramite, Permitidos, EstatusRevisionJefe,
                new NomTramiteBitacora { IdEmpleado = request.NoEmpActor, Accion = "ENVIADO A FIRMA DEL JEFE" },
                borrarDetalleVacaciones: false, cancellationToken);
            if (anterior == null) return await Result<int>.FailureAsync("El trámite no existe.");
            if (!Permitidos.Contains(anterior.Value))
                return await Result<int>.FailureAsync("Solo se puede enviar a firma un trámite Creado o Regresado.");
            return await Result<int>.SuccessAsync(EstatusRevisionJefe, "Trámite enviado a firma del jefe.");
        }
    }
}
