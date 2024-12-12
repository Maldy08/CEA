using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticoOficinaRepository : IViaticoOficinaRepository
    {
        private readonly ApplicationDbContext _context;

        public ViaticoOficinaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ViaticoOficinaDto>> GetAll()
        {
            return await _context.ViaticoOficina
                .Select(x => new ViaticoOficinaDto
                {
                    IdOfi = x.IdOfi,
                    Nombre = x.Nombre,
                    RutaTrans = x.RutaTrans,
                }).OrderBy(x => x.IdOfi).ToListAsync();
               
        }
    }
}
