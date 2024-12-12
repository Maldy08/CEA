
using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Oficios.Queries.GetOficiosMcByEor
{
    public record GetOficiosMcByEorQuery : IRequest<Result<List<OficioDto>>>
    {
        public int Eor { get; set; }

        public GetOficiosMcByEorQuery(int eor)
        {
            Eor = eor;
        }

    }
    internal class GetOficiosMcByEorQueryHandler : IRequestHandler<GetOficiosMcByEorQuery, Result<List<OficioDto>>>
    {
        private readonly IOficioRepository _oficioRepository;
        private readonly IMapper _mapper;

        public GetOficiosMcByEorQueryHandler(IOficioRepository oficioRepository, IMapper mapper)
        {
            _oficioRepository = oficioRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OficioDto>>> Handle(GetOficiosMcByEorQuery request, CancellationToken cancellationToken)
        {
            var oficios = await _oficioRepository.GetOficiosMC(request.Eor);
            var oficiosDto = _mapper.Map<List<OficioDto>>(oficios);
            return await Result<List<OficioDto>>.SuccessAsync(oficiosDto);
        }
    }

}
