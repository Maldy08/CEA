using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Oficios.Queries.GetAllOficios
{
    public record GetAllOficiosQuery : IRequest<Result<List<OficioDto>>>
    {
        public GetAllOficiosQuery()
        {
        }

    }

    internal class GetAllOficiosQueryHandler: IRequestHandler<GetAllOficiosQuery, Result<List<OficioDto>>>
    {
        private readonly IOficioRepository _oficioRepository;
        private readonly IMapper _mapper;

        public GetAllOficiosQueryHandler(IOficioRepository oficioRepository, IMapper mapper)
        {
            _oficioRepository = oficioRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OficioDto>>> Handle(GetAllOficiosQuery request, CancellationToken cancellationToken)
        {
            var oficios = await _oficioRepository.GetAllOficios();
            var oficiosDto = _mapper.Map<List<OficioDto>>(oficios);
            return await Result<List<OficioDto>>.SuccessAsync(oficiosDto);
        }
    }
    
 }

