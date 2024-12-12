using AutoMapper;
using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.UseCases.Transparencia.Queries.GetFormatos
{
    public record GetFormatosQuery : IRequest<Result<List<FormatoDto>>>;
    internal class GetFormatosQueryHandler : IRequestHandler<GetFormatosQuery, Result<List<FormatoDto>>>
    {

        private readonly IMapper _mapper;
        private readonly IFormatoRepository _formatoRepository;

        public GetFormatosQueryHandler(IFormatoRepository formatoRepository, IMapper mapper)
        {
            _formatoRepository = formatoRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<FormatoDto>>> Handle(GetFormatosQuery request, CancellationToken cancellationToken)
        {

            var entities = await _formatoRepository.GetFormatos();
            var mappedEntities = _mapper.Map<List<FormatoDto>>(entities);
            return Result<List<FormatoDto>>.Success(mappedEntities);

        }
    }
}
 