

using AutoMapper;
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Queries.GetAllViaticoPais
{
    public record GetAllViaticoPaisQuery : IRequest<Result<List<ViaticoPaisDto>>>;
    internal class GetAllViaticoPaisQueryHandler : IRequestHandler<GetAllViaticoPaisQuery, Result<List<ViaticoPaisDto>>>
    {
        private readonly IViaticoPaisRepository _viaticoPaisRepository;
        private readonly IMapper _mapper;

        public GetAllViaticoPaisQueryHandler(IViaticoPaisRepository viaticoPaisRepository, IMapper mapper)
        {
            _viaticoPaisRepository = viaticoPaisRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ViaticoPaisDto>>> Handle(GetAllViaticoPaisQuery request, CancellationToken cancellationToken)
        {
            var viaticosPais = await _viaticoPaisRepository.GetAll();  
            var mappedViaticosPais = _mapper.Map<List<ViaticoPaisDto>>(viaticosPais);
            return Result<List<ViaticoPaisDto>>.Success(mappedViaticosPais);
        }
    }
}
