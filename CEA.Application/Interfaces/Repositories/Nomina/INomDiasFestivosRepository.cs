using CEA.Application.DTOs.Nomina;

namespace CEA.Application.Interfaces.Repositories.Nomina
{
    public interface INomDiasFestivosRepository
    {
        Task<List<NomDiasFestivosDto>> GetByEmpleadoAsync(int noEmp, DateTime desde, DateTime hasta);
    }
}
