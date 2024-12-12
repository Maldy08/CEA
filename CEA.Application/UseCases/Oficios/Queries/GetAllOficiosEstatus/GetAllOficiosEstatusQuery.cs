using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.Features.Oficios.Queries.GetAllOficiosEstatus
{
    public record GetAllOficiosEstatusQuery : IRequest<Result<List<OficioEstatusDto>>> { }
    internal class GetAllOficiosEstatusQueryHanlder : IRequestHandler<GetAllOficiosEstatusQuery, Result<List<OficioEstatusDto>>>
    {
        private readonly IOficioEstatusRepository _oficioEstatusRepository;
        private readonly IMapper _mapper;

        public GetAllOficiosEstatusQueryHanlder(IOficioEstatusRepository oficioEstatusRepository, IMapper mapper)
        {
            _oficioEstatusRepository = oficioEstatusRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OficioEstatusDto>>> Handle(GetAllOficiosEstatusQuery request, CancellationToken cancellationToken)
        {
           var entities = await _oficioEstatusRepository.GetAll();
            var mappedEntities = _mapper.Map<List<OficioEstatusDto>>(entities);
            return await Result<List<OficioEstatusDto>>.SuccessAsync(mappedEntities);
        }
    }
}
