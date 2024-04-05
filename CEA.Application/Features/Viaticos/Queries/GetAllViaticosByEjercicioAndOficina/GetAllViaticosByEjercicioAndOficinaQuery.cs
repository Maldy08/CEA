using AutoMapper;
using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Queries.GetAllViaticosByEjercicioAndOficina
{

    public record GetAllViaticosByEjercicioAndOficinaQuery: IRequest<Result<List<GetAllViaticosByEjercicioAndOficinaDto>>>
    {
        public int Ejercicio { get; set; }
        public int Oficina { get; set; }

        public GetAllViaticosByEjercicioAndOficinaQuery(int ejercicio, int oficina)
        {
            Ejercicio = ejercicio;
            Oficina = oficina;
        }
    }


    internal class GetAllViaticosByEjercicioAndOficinaHandler : IRequestHandler<GetAllViaticosByEjercicioAndOficinaQuery, Result<List<GetAllViaticosByEjercicioAndOficinaDto>>>
    {
        private readonly IViaticoRepository _viaticoRepository;
        private readonly IMapper _mapper;

        public GetAllViaticosByEjercicioAndOficinaHandler( IViaticoRepository viaticoRepository, IMapper mapper)
        {
            _viaticoRepository = viaticoRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<GetAllViaticosByEjercicioAndOficinaDto>>> Handle(GetAllViaticosByEjercicioAndOficinaQuery query, CancellationToken cancellationToken)
        {
            var entities = await _viaticoRepository.GetAllViaticosByEjercicioAndOficina(query.Ejercicio, query.Oficina);
            var viaticos = _mapper.Map<List<GetAllViaticosByEjercicioAndOficinaDto>>(entities);
            return await Result<List<GetAllViaticosByEjercicioAndOficinaDto>>.SuccessAsync(viaticos);
        }
    }
}
