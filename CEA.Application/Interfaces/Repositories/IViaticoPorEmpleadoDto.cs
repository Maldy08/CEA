using CEA.Application.DTOs;


namespace CEA.Application.Interfaces.Repositories
{
    public interface IViaticoPorEmpleadoDto
    {
        Task<List<ViaticosPorEmpleadoDto>> GetListadoPorEjercicioAndEmpleado(int ejercicio, int empleado);
    }
}
