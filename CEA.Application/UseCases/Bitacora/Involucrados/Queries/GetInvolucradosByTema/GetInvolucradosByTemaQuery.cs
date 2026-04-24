using AutoMapper;
using CEA.Application.DTOs.Bitacora;
using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Involucrados.Queries.GetInvolucradosByTema
{
    public record GetInvolucradosByTemaQuery(int IdTema) : IRequest<Result<IEnumerable<TemaInvolucradoDto>>>;

    internal class GetInvolucradosByTemaQueryHandler : IRequestHandler<GetInvolucradosByTemaQuery, Result<IEnumerable<TemaInvolucradoDto>>>
    {
        private readonly ITemaInvolucradoRepository _involucradoRepository;
        private readonly IMapper _mapper;

        public GetInvolucradosByTemaQueryHandler(ITemaInvolucradoRepository involucradoRepository, IMapper mapper)
        {
            _involucradoRepository = involucradoRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TemaInvolucradoDto>>> Handle(GetInvolucradosByTemaQuery request, CancellationToken cancellationToken)
        {
            var involucrados = await _involucradoRepository.GetByTemaAsync(request.IdTema);
            var involucradosDto = _mapper.Map<IEnumerable<TemaInvolucradoDto>>(involucrados);
            return Result<IEnumerable<TemaInvolucradoDto>>.Success(involucradosDto);
        }
    }
}
