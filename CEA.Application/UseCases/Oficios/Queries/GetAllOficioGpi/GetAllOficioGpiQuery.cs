using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetAllOficioGpi
{
    public record GetAllOficioGpiQuery : IRequest<Result<IEnumerable<OficioGpiDto>>> { }
    internal class GetAllOficioGpiQueryHandler : IRequestHandler<GetAllOficioGpiQuery, Result<IEnumerable<OficioGpiDto>>>
    {

        private readonly IOficioGpiRepository _oficioGpiRepository;
        private readonly IMapper _mapper;

        public GetAllOficioGpiQueryHandler(IOficioGpiRepository oficioGpiRepository, IMapper mapper)
        {
            _oficioGpiRepository = oficioGpiRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<OficioGpiDto>>> Handle(GetAllOficioGpiQuery request, CancellationToken cancellationToken)
        {
            var entities = await _oficioGpiRepository.GetAllOficioGpi();
            var dtos = _mapper.Map<IEnumerable<OficioGpiDto>>(entities);
            return Result<IEnumerable<OficioGpiDto>>.Success(dtos);
        }
    
    
    }
}
