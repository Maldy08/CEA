using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetContadoresDashboardByEjercicioEmpleado
{
    public record GetContadoresDashboardByEjercicioEmpleadoQuery : IRequest<Result<List<OficioContadoresDashboardDto>>>
    {
        public int Ejercicio { get; init; }
        public int Empleado { get; init; }

        public GetContadoresDashboardByEjercicioEmpleadoQuery(int ejercicio, int empleado)
        {
            Ejercicio = ejercicio;
            Empleado = empleado;
        }
    }
    internal class GetContadoresDashboardByEjercicioEmpleadoQueryHandler : IRequestHandler<GetContadoresDashboardByEjercicioEmpleadoQuery, Result<List<OficioContadoresDashboardDto>>>
    {

        private readonly IOficioFunctions _oficioFunctions;

        public GetContadoresDashboardByEjercicioEmpleadoQueryHandler(IOficioFunctions oficioFunctions)
        {
            _oficioFunctions = oficioFunctions;
        }

        public async Task<Result<List<OficioContadoresDashboardDto>>> Handle(GetContadoresDashboardByEjercicioEmpleadoQuery request, CancellationToken cancellationToken)
        {
            var entities = await _oficioFunctions.GetContadoresDashboard(request.Ejercicio, request.Empleado);
            return await Result<List<OficioContadoresDashboardDto>>.SuccessAsync(entities);
        }
    }
}
