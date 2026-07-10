using CEA.Application.DTOs.IndiArct;
using CEA.Domain.Entities.IndiArct;

namespace CEA.Application.Interfaces.Repositories.IndiArct
{
    public interface IIndiArctCatpresasRepository
    {
        Task<IEnumerable<IndiArctCatpresasDto>> GetAllAsync();
        Task<IndiArctCatpresas?> GetByIdAsync(int id);
        Task<IndiArctCatpresas> AddAsync(IndiArctCatpresas entity);
        Task UpdateAsync(IndiArctCatpresas entity);
        Task DeleteAsync(int id);
    }
}
