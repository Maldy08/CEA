
using CEA.Application.DTOs;

namespace CEA.Application.Interfaces.Repositories
{
    public interface IDeptoRepository
    {
        Task<IEnumerable<DeptoUeDto>> GetDeptosAsync();
        Task<DeptoUeDto> GetDeptoByIdAsync(int id);
    }
}
