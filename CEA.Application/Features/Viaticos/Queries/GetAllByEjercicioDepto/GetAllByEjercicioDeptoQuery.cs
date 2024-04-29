
using AutoMapper;
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Queries.GetAllByEjercicioDepto
{

    public record GetAllByEjercicioDeptoQuery: IRequest<Result<List<GetAllViaticosDto>>>
    {
        public int Ejercicio { get; set; }
        public int Empleado { get; set; }

        public GetAllByEjercicioDeptoQuery(int ejercicio, int empleado)
        {
            Ejercicio = ejercicio;
            Empleado = empleado;
        }
    }

    internal class GetAllByEjercicioDeptoHandler : IRequestHandler<GetAllByEjercicioDeptoQuery, Result<List<GetAllViaticosDto>>>
    {
        private readonly IViaticoRepository _viaticoRepository;
        private readonly IMapper _mapper;

        public GetAllByEjercicioDeptoHandler( IViaticoRepository viaticoRepository, IMapper mapper)
        {
            _viaticoRepository = viaticoRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<GetAllViaticosDto>>> Handle(GetAllByEjercicioDeptoQuery query, CancellationToken cancellationToken)
        {
            var entities = await _viaticoRepository.GetAllByEjercicioDepto(query.Ejercicio, query.Empleado);
            var viaticos = _mapper.Map<List<GetAllViaticosDto>>(entities);
            return await Result<List<GetAllViaticosDto>>.SuccessAsync(viaticos);
        }
    }
    
    
}
