

using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Oficios.Queries.GetAllOficiosUsuExt
{
    public record GetAllOficiosUsuExtQueryManto : IRequest<Result<List<OficioUsuExtDto>>>
    {


    }
    internal class GetAllOficiosUsuExtQueryMantoHandler : IRequestHandler<GetAllOficiosUsuExtQueryManto, Result<List<OficioUsuExtDto>>>
    {
        private readonly IOficioUsuExtRepository _oficioUsuExtRepository;
        private readonly IMapper _mapper;

        public GetAllOficiosUsuExtQueryMantoHandler(IOficioUsuExtRepository oficioUsuExtRepository, IMapper mapper)
        {
            _oficioUsuExtRepository = oficioUsuExtRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OficioUsuExtDto>>> Handle(GetAllOficiosUsuExtQueryManto request, CancellationToken cancellationToken)
        {
            var entities = await _oficioUsuExtRepository.GetOficiosUsuariosExternosMantenimiento();
            var mappedEntities = _mapper.Map<List<OficioUsuExtDto>>(entities);
            return Result<List<OficioUsuExtDto>>.Success(mappedEntities);
        }
    }
}
