using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Domain.Entities.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Commands.CancelarTramite
{
    // Estatus 99 de la pestaña "Procesos": UPDATE NOM_TRAMITES (FECHA_CANCELACION) + INSERT bitácora + DELETE
    // NOM_VACAEMP_D (los días vuelven al saldo; NOM_VACAEMP_M se conserva como historial).
    // Lo cancela el solicitante mientras no lo haya autorizado su jefe: Creado (1), Revisión Jefe (2) o Regresado (98).
    public record CancelarTramiteCommand : IRequest<Result<int>>
    {
        public int IdTramite { get; init; }
        public int NoEmpActor { get; init; }
        // Motivo de la cancelación (obligatorio).
        public string Comentarios { get; init; } = string.Empty;
    }

    internal class CancelarTramiteCommandHandler : IRequestHandler<CancelarTramiteCommand, Result<int>>
    {
        private static readonly int[] Permitidos = { 1, 2, 98 };
        private const int EstatusCancelado = 99;
        private const int TipoVacaciones = 1;

        private readonly INomTramitesRepository _tramites;

        public CancelarTramiteCommandHandler(INomTramitesRepository tramites)
        {
            _tramites = tramites;
        }

        public async Task<Result<int>> Handle(CancelarTramiteCommand request, CancellationToken cancellationToken)
        {
            var motivo = request.Comentarios?.Trim() ?? "";
            if (motivo.Length == 0) return await Result<int>.FailureAsync("Escriba el motivo de la cancelación.");
            if (motivo.Length > 1000) return await Result<int>.FailureAsync("El motivo no puede pasar de 1000 caracteres.");

            var tramite = await _tramites.GetByIdAsync(request.IdTramite);
            if (tramite == null) return await Result<int>.FailureAsync("El trámite no existe.");

            var anterior = await _tramites.CambiarEstatusAsync(request.IdTramite, Permitidos, EstatusCancelado,
                new NomTramiteBitacora { IdEmpleado = request.NoEmpActor, Accion = "TRAMITE CANCELADO", Comentarios = motivo },
                borrarDetalleVacaciones: tramite.TipoTramite == TipoVacaciones, cancellationToken);
            if (anterior == null) return await Result<int>.FailureAsync("El trámite no existe.");
            if (!Permitidos.Contains(anterior.Value))
                return await Result<int>.FailureAsync(anterior == EstatusCancelado
                    ? "El trámite ya estaba cancelado."
                    : "El trámite ya fue autorizado por el jefe; ya no se puede cancelar desde aquí.");
            return await Result<int>.SuccessAsync(EstatusCancelado, "Trámite cancelado.");
        }
    }
}
