using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetTramitesEmpleado
{
    public record GetTramitesEmpleadoQuery : IRequest<Result<List<NomTramiteDto>>>
    {
        public int NoEmp { get; init; }
        public int Ano { get; init; }
        // Nulo = todos los tipos.
        public int? TipoTramite { get; init; }

        public GetTramitesEmpleadoQuery(int noEmp, int ano, int? tipoTramite)
        {
            NoEmp = noEmp;
            Ano = ano;
            TipoTramite = tipoTramite;
        }
    }
    internal class GetTramitesEmpleadoQueryHandler : IRequestHandler<GetTramitesEmpleadoQuery, Result<List<NomTramiteDto>>>
    {
        private readonly INomTramitesRepository _nomTramitesRepository;

        public GetTramitesEmpleadoQueryHandler(INomTramitesRepository nomTramitesRepository)
        {
            _nomTramitesRepository = nomTramitesRepository;
        }

        public async Task<Result<List<NomTramiteDto>>> Handle(GetTramitesEmpleadoQuery request, CancellationToken cancellationToken)
        {
            var tramites = await _nomTramitesRepository.GetByEmpleadoAsync(request.NoEmp, request.Ano, request.TipoTramite);
            return await Result<List<NomTramiteDto>>.SuccessAsync(tramites);
        }
    }
}
