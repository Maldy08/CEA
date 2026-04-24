using CEA.Domain.Entities.Bitacora;

namespace CEA.Application.Interfaces.Repositories.Bitacora
{
    public interface ITemaInvolucradoRepository
    {
        Task<IEnumerable<TemaInvolucrado>> GetByTemaAsync(int idTema);
        Task<TemaInvolucrado> AddAsync(TemaInvolucrado involucrado);
        Task RemoveAsync(int idTema, int idUsuario);
    }
}
