using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficioEstatusByEor
{

    public record GetOficioEstatusByEorQuery : IRequest<Result<List<OficioEstatusDto>>>
    {
        public int EorId { get; init; }
        public GetOficioEstatusByEorQuery(int eorId)
        {
            EorId = eorId;
        }
    }

    internal class GetOficioEstatusByEorQueryHandler : IRequestHandler<GetOficioEstatusByEorQuery, Result<List<OficioEstatusDto>>>
    {
        private readonly IOficioRepository _oficioRepository;
        private readonly IMapper _mapper;

        public GetOficioEstatusByEorQueryHandler(IOficioRepository oficioRepository, IMapper mapper)
        {
            _oficioRepository = oficioRepository;
            _mapper = mapper;
        }


        public async Task<Result<List<OficioEstatusDto>>> Handle(GetOficioEstatusByEorQuery request, CancellationToken cancellationToken)
        {
            var entities = await _oficioRepository.GetEstatusOficiosByEor(request.EorId);
            var mappedEntities = _mapper.Map<List<OficioEstatusDto>>(entities);
            return Result<List<OficioEstatusDto>>.Success(mappedEntities);

        }
    }
}
