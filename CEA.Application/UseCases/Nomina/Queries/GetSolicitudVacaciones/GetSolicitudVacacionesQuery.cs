using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetSolicitudVacaciones
{
    public record GetSolicitudVacacionesQuery : IRequest<Result<SolicitudVacacionesDto>>
    {
        public int IdTramite { get; init; }

        public GetSolicitudVacacionesQuery(int idTramite)
        {
            IdTramite = idTramite;
        }
    }
    internal class GetSolicitudVacacionesQueryHandler : IRequestHandler<GetSolicitudVacacionesQuery, Result<SolicitudVacacionesDto>>
    {
        private readonly INomTramitesRepository _nomTramitesRepository;

        public GetSolicitudVacacionesQueryHandler(INomTramitesRepository nomTramitesRepository)
        {
            _nomTramitesRepository = nomTramitesRepository;
        }

        public async Task<Result<SolicitudVacacionesDto>> Handle(GetSolicitudVacacionesQuery request, CancellationToken cancellationToken)
        {
            var solicitud = await _nomTramitesRepository.GetSolicitudVacacionesAsync(request.IdTramite);
            if (solicitud == null)
            {
                return await Result<SolicitudVacacionesDto>.FailureAsync($"El trámite {request.IdTramite} no tiene solicitud de vacaciones.");
            }
            return await Result<SolicitudVacacionesDto>.SuccessAsync(solicitud);
        }
    }
}
