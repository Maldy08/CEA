using AutoMapper;
using CEA.Application.DTOs.Bitacora;
using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Avances.Queries.GetAvancesByTema
{
    public record GetAvancesByTemaQuery(int IdTema) : IRequest<Result<IEnumerable<AvanceDto>>>;

    internal class GetAvancesByTemaQueryHandler : IRequestHandler<GetAvancesByTemaQuery, Result<IEnumerable<AvanceDto>>>
    {
        private readonly IAvanceRepository _avanceRepository;
        private readonly IMapper _mapper;

        public GetAvancesByTemaQueryHandler(IAvanceRepository avanceRepository, IMapper mapper)
        {
            _avanceRepository = avanceRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AvanceDto>>> Handle(GetAvancesByTemaQuery request, CancellationToken cancellationToken)
        {
            var avances = await _avanceRepository.GetByTemaAsync(request.IdTema);
            var avancesDto = new List<AvanceDto>();

            foreach (var avance in avances)
            {
                var dto = _mapper.Map<AvanceDto>(avance);
                var adjuntos = await _avanceRepository.GetAdjuntosByAvanceAsync(avance.Id);
                dto.Adjuntos = adjuntos.Select(a => new AdjuntoDto
                {
                    IdAdjunto = a.Id,
                    Nombre = a.Nombre,
                    Url = a.Url,
                    TipoMime = a.TipoMime
                }).ToList();
                avancesDto.Add(dto);
            }

            return Result<IEnumerable<AvanceDto>>.Success(avancesDto);
        }
    }
}
