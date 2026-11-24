using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetTramitesPersonal
{
    // Bandeja del supervisor (TRAMITES_NIVEL 2): trámites de su personal a cargo (EMPLEADOS.SUPERVISOR_ID = él).
    // Con Capturados: los que el supervisor capturó para otros (ID_EMPLEADO_SUPERVISOR = él), p. ej. en suplencia.
    // Los firmantes (nivel 3) no la usan: ven los trámites hasta que se los envían a firma (NomTramitesFirmas).
    public record GetTramitesPersonalQuery(int NoEmpSupervisor, int Ano, int? TipoTramite, bool Capturados = false)
        : IRequest<Result<List<NomTramiteDto>>>;

    internal class GetTramitesPersonalQueryHandler : IRequestHandler<GetTramitesPersonalQuery, Result<List<NomTramiteDto>>>
    {
        private readonly INomTramitesRepository _nomTramitesRepository;
        private readonly INomTramitesFunctions _nomTramitesFunctions;

        public GetTramitesPersonalQueryHandler(INomTramitesRepository nomTramitesRepository, INomTramitesFunctions nomTramitesFunctions)
        {
            _nomTramitesRepository = nomTramitesRepository;
            _nomTramitesFunctions = nomTramitesFunctions;
        }

        public async Task<Result<List<NomTramiteDto>>> Handle(GetTramitesPersonalQuery request, CancellationToken cancellationToken)
        {
            if (!await _nomTramitesFunctions.EsSupervisor(request.NoEmpSupervisor))
                return await Result<List<NomTramiteDto>>.FailureAsync("No tiene personal a su cargo.");
            var tramites = request.Capturados
                ? await _nomTramitesRepository.GetCapturadosAsync(request.NoEmpSupervisor, request.Ano, request.TipoTramite)
                : await _nomTramitesRepository.GetDePersonalAsync(request.NoEmpSupervisor, request.Ano, request.TipoTramite);
            return await Result<List<NomTramiteDto>>.SuccessAsync(tramites);
        }
    }
}
