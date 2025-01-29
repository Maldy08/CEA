

using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Oficios
{
    public class OficioParametroRepository : IOficioParametroRepository
    {
        private readonly ApplicationDbContext _context;

        public OficioParametroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public  async Task<OficioParametroDto> GetOficioParametroByEjercicio(int ejercicio)
        {
            return await _context.OficioParametro.Select(x => new OficioParametroDto
            {
             
                Id = x.Id,
                Ejercicio = x.Ejercicio,
                NextFRec = x.NextFRec,
                NextFEnv = x.NextFEnv,
                NextFXexp = x.NextFXexp,
                
            }).FirstOrDefaultAsync(x => x.Ejercicio == ejercicio);
        }
    }
}
