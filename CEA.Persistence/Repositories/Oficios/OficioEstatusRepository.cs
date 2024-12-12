

using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Oficios
{
    public class OficioEstatusRepository : IOficioEstatusRepository
    {
        private readonly ApplicationDbContext _context;

        public OficioEstatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public  async Task<List<OficioEstatusDto>> GetAll()
        {
            return await _context.OficioEstatus.Select(x => new OficioEstatusDto
            {
                Eor = x.Eor,
                IdEstatus = x.IdEstatus,
                Nombre = x.Nombre,
            }).OrderBy(x => x.IdEstatus).ToListAsync();
        }

        public async Task<OficioEstatusDto> GetEstatusByIdEor(int id, int eor)
        {
            return await _context.OficioEstatus
                .Where(x => x.IdEstatus == id && x.Eor == eor)
                .Select(x => new OficioEstatusDto
                {
                    Eor = x.Eor,
                    IdEstatus = x.IdEstatus,
                    Nombre = x.Nombre,
                })
                .FirstOrDefaultAsync();
        }
    }
}
