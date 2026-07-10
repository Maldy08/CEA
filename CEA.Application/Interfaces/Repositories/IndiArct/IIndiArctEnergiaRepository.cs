using CEA.Application.DTOs.IndiArct;
using CEA.Domain.Entities.IndiArct;

namespace CEA.Application.Interfaces.Repositories.IndiArct
{
    public interface IIndiArctEnergiaRepository
    {
        Task<IEnumerable<IndiArctEnergiaDto>> GetAllAsync();
        Task<IEnumerable<EnergiaCapturaDto>> GetGridAsync(int anio);
        Task UpsertLoteAsync(int anio, IEnumerable<EnergiaMesItem> items);
        Task<IndiArctEnergia?> GetByIdAsync(int id);
        Task<IndiArctEnergia> AddAsync(IndiArctEnergia entity);
        Task UpdateAsync(IndiArctEnergia entity);
        Task DeleteAsync(int id);
    }
}
