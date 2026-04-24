using CEA.Application.DTOs.Bitacora;
using CEA.Domain.Entities.Bitacora;

namespace CEA.Application.Interfaces.Repositories.Bitacora
{
    public interface ITemaRepository
    {
        Task<IEnumerable<Tema>> GetAllAsync();
        Task<IEnumerable<TemaDto>> GetAllConContadoresAsync();
        Task<Tema?> GetByIdAsync(int id);
        Task<IEnumerable<Tema>> GetByUsuarioAsync(int idUsuario);
        Task<IEnumerable<TemaDto>> GetByUsuarioConContadoresAsync(int idUsuario);
        Task<Tema> AddAsync(Tema tema);
        Task UpdateAsync(Tema tema);
        Task DeleteAsync(int id);
        Task UpdateEstadoAsync(int id, string estado);
    }
}
