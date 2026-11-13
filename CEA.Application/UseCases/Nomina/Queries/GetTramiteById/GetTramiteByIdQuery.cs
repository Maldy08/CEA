using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetTramiteById
{
    public record GetTramiteByIdQuery : IRequest<Result<NomTramiteDto>>
    {
        public int IdTramite { get; init; }

        public GetTramiteByIdQuery(int idTramite)
        {
            IdTramite = idTramite;
        }
    }
    internal class GetTramiteByIdQueryHandler : IRequestHandler<GetTramiteByIdQuery, Result<NomTramiteDto>>
    {
        private readonly INomTramitesRepository _nomTramitesRepository;

        public GetTramiteByIdQueryHandler(INomTramitesRepository nomTramitesRepository)
        {
            _nomTramitesRepository = nomTramitesRepository;
        }

        public async Task<Result<NomTramiteDto>> Handle(GetTramiteByIdQuery request, CancellationToken cancellationToken)
        {
            var tramite = await _nomTramitesRepository.GetByIdAsync(request.IdTramite);
            if (tramite == null)
            {
                return await Result<NomTramiteDto>.FailureAsync($"El trámite {request.IdTramite} no existe.");
            }
            return await Result<NomTramiteDto>.SuccessAsync(tramite);
        }
    }
}
