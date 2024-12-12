

using CEA.Application.DTOs;

namespace CEA.Application.Interfaces.Repositories
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<EmpleadoDto>> GetEmpleadosAsync();
        Task<EmpleadoDto> GetEmpleadoByIdAsync(int id);
        Task<List<EmpleadoDto>> GetEmpleadosByDeptoPpto(int id);
        Task<List<EmpleadoDto>> GetEmpleadosByDeptoComi(int id);
 
    }
}
