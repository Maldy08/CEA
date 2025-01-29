using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetListaDashboardByEjercicioEmpleado
{
    public record GetListaDashboardByEjercicioEmpleadoQuery : IRequest<Result<List<OficioListaDashboardDto>>>
    {
        public int Ejercicio { get; init; }
        public int Empleado { get; init; }

        public GetListaDashboardByEjercicioEmpleadoQuery(int ejercicio, int empleado)
        {
            Ejercicio = ejercicio;
            Empleado = empleado;
        }
    }

    internal class GetListaDashboardByEjercicioEmpleadoQueryHandler : IRequestHandler<GetListaDashboardByEjercicioEmpleadoQuery, Result<List<OficioListaDashboardDto>>>
    {

        private readonly IOficioFunctions oficioFunctions;

        public GetListaDashboardByEjercicioEmpleadoQueryHandler(IOficioFunctions oficioFunctions)
        {
            this.oficioFunctions = oficioFunctions;
        }
        public async Task<Result<List<OficioListaDashboardDto>>> Handle(GetListaDashboardByEjercicioEmpleadoQuery request, CancellationToken cancellationToken)
        {
            var entities = await oficioFunctions.GetListaDashboard(request.Ejercicio, request.Empleado);
            return await Result<List<OficioListaDashboardDto>>.SuccessAsync(entities);
        }
    }
    
    
}
