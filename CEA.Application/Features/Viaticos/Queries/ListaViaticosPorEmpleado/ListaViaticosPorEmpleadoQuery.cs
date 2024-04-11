
using AutoMapper;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Queries.ListaViaticosPorEmpleado
{

    public record ListaViatosPorEmpleadoQuery : IRequest<Result<List<ViaticosPorEmpleadoDto>>>
    {
        public int Ejercicio { get; set; }
        public int Empleado { get; set; }

        public ListaViatosPorEmpleadoQuery(int ejercicio, int empleado)
        {
            Ejercicio = ejercicio;
            Empleado = empleado;
        }
    }

    internal class ListaViaticosPorEmpleadoHandler : IRequestHandler<ListaViatosPorEmpleadoQuery, Result<List<ViaticosPorEmpleadoDto>>>
    {
        private readonly IViaticoPorEmpleadoDto _viaticoRepository;
        private readonly IMapper _mapper;

        public ListaViaticosPorEmpleadoHandler(IViaticoPorEmpleadoDto viaticoRepository, IMapper mapper)
        {
            _viaticoRepository = viaticoRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<ViaticosPorEmpleadoDto>>> Handle(ListaViatosPorEmpleadoQuery request, CancellationToken cancellationToken)
        {
            var entities = await _viaticoRepository.GetListadoPorEjercicioAndEmpleado(request.Ejercicio, request.Empleado);
            var viaticos = _mapper.Map<List<ViaticosPorEmpleadoDto>>(entities);
            return await Result<List<ViaticosPorEmpleadoDto>>.SuccessAsync(viaticos);
        }
    }
}
