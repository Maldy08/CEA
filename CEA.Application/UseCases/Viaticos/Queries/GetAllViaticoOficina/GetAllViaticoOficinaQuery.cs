

using AutoMapper;
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Queries.GetAllViaticoOficina
{
    public record GetAllViaticoOficinaQuery : IRequest<Result<List<ViaticoOficinaDto>>>;
    internal class GetAllViaticoOficinaQueryHandler : IRequestHandler<GetAllViaticoOficinaQuery, Result<List<ViaticoOficinaDto>>>
    {
        private readonly IViaticoOficinaRepository _viaticoOficinaRepository;
        private readonly IMapper _mapper;

        public GetAllViaticoOficinaQueryHandler(IViaticoOficinaRepository viaticoOficinaRepository, IMapper mapper)
        {
            _viaticoOficinaRepository = viaticoOficinaRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<ViaticoOficinaDto>>> Handle(GetAllViaticoOficinaQuery request, CancellationToken cancellationToken)
        {
            var viaticoOficinas = await _viaticoOficinaRepository.GetAll();
            var mappedViaticoOficinas = _mapper.Map<List<ViaticoOficinaDto>>(viaticoOficinas);
            return Result<List<ViaticoOficinaDto>>.Success(mappedViaticoOficinas);
        }
    }
}
