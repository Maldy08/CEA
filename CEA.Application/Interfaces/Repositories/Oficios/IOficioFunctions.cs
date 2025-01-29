using CEA.Application.DTOs.Oficios;

namespace CEA.Application.Interfaces.Repositories.Oficios
{
    public interface IOficioFunctions
    {
        Task<List<OficioDtoFunction>> GetListadoOficioFunction(int ejercicio, int eor, int idEmpleado);
        Task<List<OficioContadoresDashboardDto>> GetContadoresDashboard(int ejercicio, int idEmpleado);
        Task<List<OficioListaDashboardDto>> GetListaDashboard(int ejercicio, int idEmpleado);
    }
}
