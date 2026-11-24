using CEA.Application.DTOs.Viaticos;


namespace CEA.Application.Interfaces.Repositories.Viaticos
{
    public interface IViaticoPorEmpleadoDto
    {
        Task<List<ViaticosPorEmpleadoDto>> GetListadoPorEjercicioAndEmpleado(int ejercicio, int empleado);
        // Viáticos del ejercicio que el usuario capturó para otros empleados (NOEMP_CREA = él, NOEMP distinto).
        Task<List<ViaticoCapturadoDto>> GetCapturadosAsync(int ejercicio, int noEmpCrea);
    }
}
