using CEA.Application.DTOs.IndiArct;
using CEA.Domain.Entities.IndiArct;

namespace CEA.Application.Interfaces.Repositories.IndiArct
{
    public interface IIndiArctPresasNivelesRepository
    {
        Task<IEnumerable<IndiArctPresasNivelesDto>> GetAllAsync();
        Task<IEnumerable<IndiArctPresasNivelesDto>> GetByPresaAsync(int idPresa);
        Task<IEnumerable<IndiArctPresasNivelesDto>> GetByAnioAsync(int anio);
        Task<IEnumerable<PresaCapturaDto>> GetGridAsync(int anio, int mes);
        Task UpsertLoteAsync(int anio, int mes, IEnumerable<PresaVolumenItem> items);
        Task<IndiArctPresasNiveles?> GetByIdAsync(int id);
        Task<IndiArctPresasNiveles> AddAsync(IndiArctPresasNiveles entity);
        Task UpdateAsync(IndiArctPresasNiveles entity);
        Task DeleteAsync(int id);
    }
}
