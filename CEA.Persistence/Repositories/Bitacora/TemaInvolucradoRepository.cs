using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Domain.Entities.Bitacora;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Bitacora
{
    public class TemaInvolucradoRepository : ITemaInvolucradoRepository
    {
        private readonly ApplicationDbContext _context;

        public TemaInvolucradoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TemaInvolucrado>> GetByTemaAsync(int idTema)
            => await _context.TemaInvolucrados.Where(ti => ti.IdTema == idTema).ToListAsync();

        public async Task<TemaInvolucrado> AddAsync(TemaInvolucrado involucrado)
        {
            await _context.TemaInvolucrados.AddAsync(involucrado);
            await _context.SaveChangesAsync();
            return involucrado;
        }

        public async Task RemoveAsync(int idTema, int idUsuario)
        {
            var involucrado = await _context.TemaInvolucrados
                .FirstOrDefaultAsync(ti => ti.IdTema == idTema && ti.IdUsuario == idUsuario);
            if (involucrado == null) return;
            _context.TemaInvolucrados.Remove(involucrado);
            await _context.SaveChangesAsync();
        }
    }
}
