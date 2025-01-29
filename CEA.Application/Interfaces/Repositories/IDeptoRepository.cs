
using CEA.Application.DTOs;
using CEA.Application.DTOs.Oficios;

namespace CEA.Application.Interfaces.Repositories
{
    public interface IDeptoRepository
    {
        Task<IEnumerable<DeptoUeDto>> GetDeptosAsync();
        Task<DeptoUeDto> GetDeptoByIdAsync(int id);
        Task<List<OficioListaDepartamentosDto>> GetDepartamentosAsync(int depto, int ejercicio);
    }
}
