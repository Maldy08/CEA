using AutoMapper;
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.Features.Viaticos.Queries.GetViaticoPartByOficinaEjercicioNoviatPartida
{

    public record GetViaticoPartByOficinaEjercicioNoviatPartidaQuery : IRequest<Result<ViaticoPartDto>>
    {
        public int Oficina { get; set; }
        public int Ejercicio { get; set; }
        public int Noviat { get; set; }
        public int Partida { get; set; }

        public GetViaticoPartByOficinaEjercicioNoviatPartidaQuery(int oficina, int ejercicio, int noviat, int partida)
        {
            Oficina = oficina;
            Ejercicio = ejercicio;
            Noviat = noviat;
            Partida = partida;
        }
    }
    internal class GetViaticoPartByOficinaEjercicioNoviatPartidaHandler : IRequestHandler<GetViaticoPartByOficinaEjercicioNoviatPartidaQuery, Result<ViaticoPartDto>>
    {
        private readonly IViaticoPartRepository _viaticoPartRepository;
        private readonly IMapper _mapper;

        public GetViaticoPartByOficinaEjercicioNoviatPartidaHandler(IViaticoPartRepository viaticoPartRepository, IMapper mapper)
        {
            _viaticoPartRepository = viaticoPartRepository;
            _mapper = mapper;
        }
        public async Task<Result<ViaticoPartDto>> Handle(GetViaticoPartByOficinaEjercicioNoviatPartidaQuery query, CancellationToken cancellationToken)
        {
            var entity = await _viaticoPartRepository.GetByOficinaEjercicioNoviatPartida(query.Oficina, query.Ejercicio, query.Noviat, query.Partida);
            var viaticoPart = _mapper.Map<ViaticoPartDto>(entity);
            return await Result<ViaticoPartDto>.SuccessAsync(viaticoPart);
        }
    }
}
