using CEA.Application.DTOs.Viaticos;


namespace CEA.Application.Interfaces.Repositories.Viaticos
{
    public interface IViaticoPorEmpleadoDto
    {
        Task<List<ViaticosPorEmpleadoDto>> GetListadoPorEjercicioAndEmpleado(int ejercicio, int empleado);
    }
}
