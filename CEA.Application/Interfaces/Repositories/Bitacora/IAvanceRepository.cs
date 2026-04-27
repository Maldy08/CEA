using CEA.Domain.Entities.Bitacora;

namespace CEA.Application.Interfaces.Repositories.Bitacora
{
    public interface IAvanceRepository
    {
        Task<IEnumerable<Avance>> GetByTemaAsync(int idTema);
        Task<IEnumerable<Adjunto>> GetAdjuntosByAvanceAsync(int idAvance);
        Task<Avance> AddAsync(Avance avance);
        Task<Adjunto> AddAdjuntoAsync(Adjunto adjunto);
        Task UpdateObservacionesAsync(int id, string observaciones);
        Task DeleteAsync(int id);
    }
}
