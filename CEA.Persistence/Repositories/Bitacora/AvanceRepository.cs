using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Domain.Entities.Bitacora;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Bitacora
{
    public class AvanceRepository : IAvanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AvanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Avance>> GetByTemaAsync(int idTema)
            => await _context.Avances.Where(a => a.IdTema == idTema).OrderByDescending(a => a.FechaHora).ToListAsync();

        public async Task<IEnumerable<Adjunto>> GetAdjuntosByAvanceAsync(int idAvance)
            => await _context.Adjuntos.Where(a => a.IdAvance == idAvance).ToListAsync();

        public async Task<Avance> AddAsync(Avance avance)
        {
            await _context.Avances.AddAsync(avance);
            await _context.SaveChangesAsync();
            return avance;
        }

        public async Task<Adjunto> AddAdjuntoAsync(Adjunto adjunto)
        {
            await _context.Adjuntos.AddAsync(adjunto);
            await _context.SaveChangesAsync();
            return adjunto;
        }

        public async Task UpdateObservacionesAsync(int id, string observaciones)
        {
            var avance = await _context.Avances.FindAsync(id);
            if (avance == null) return;
            avance.Observaciones = observaciones;
            avance.FechaEdicion = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var avance = await _context.Avances.FindAsync(id);
            if (avance == null) return;
            var adjuntos = await _context.Adjuntos.Where(a => a.IdAvance == id).ToListAsync();
            _context.Adjuntos.RemoveRange(adjuntos);
            _context.Avances.Remove(avance);
            await _context.SaveChangesAsync();
        }
    }
}
