using AutoMapper;
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Queries.GetAllViaticosByEjercicioAndOficina
{

    public record GetAllViaticosByEjercicioAndOficinaQuery: IRequest<Result<List<GetAllViaticosDto>>>
    {
        public int Ejercicio { get; set; }
        public int Oficina { get; set; }

        public GetAllViaticosByEjercicioAndOficinaQuery(int ejercicio, int oficina)
        {
            Ejercicio = ejercicio;
            Oficina = oficina;
        }
    }

    internal class GetAllViaticosByEjercicioAndOficinaHandler : IRequestHandler<GetAllViaticosByEjercicioAndOficinaQuery, Result<List<GetAllViaticosDto>>>
    {
        private readonly IViaticoRepository _viaticoRepository;
        private readonly IMapper _mapper;

        public GetAllViaticosByEjercicioAndOficinaHandler( IViaticoRepository viaticoRepository, IMapper mapper)
        {
            _viaticoRepository = viaticoRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<GetAllViaticosDto>>> Handle(GetAllViaticosByEjercicioAndOficinaQuery query, CancellationToken cancellationToken)
        {
            var entities = await _viaticoRepository.GetAllViaticosByEjercicioAndOficina(query.Ejercicio, query.Oficina);
            var viaticos = _mapper.Map<List<GetAllViaticosDto>>(entities);
            return await Result<List<GetAllViaticosDto>>.SuccessAsync(viaticos);
        }
    }
}
