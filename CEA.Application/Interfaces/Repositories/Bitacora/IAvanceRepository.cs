using CEA.Domain.Entities.Bitacora;

namespace CEA.Application.Interfaces.Repositories.Bitacora
{
    public interface IAvanceRepository
    {
        Task<IEnumerable<Avance>> GetByTemaAsync(int idTema);
        Task<IEnumerable<Adjunto>> GetAdjuntosByAvanceAsync(int idAvance);
        Task<Avance?> GetByIdAsync(int id);
        Task<Avance?> GetUltimoByTemaAsync(int idTema);
        Task<Adjunto?> GetAdjuntoByIdAsync(int id);
        Task<Avance> AddAsync(Avance avance);
        Task<Adjunto> AddAdjuntoAsync(Adjunto adjunto);
        Task UpdateAsync(int id, string observaciones, string estado);
        Task DeleteAsync(int id);
    }
}
