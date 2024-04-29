using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Viaticos
{
    public class FormatoComisionRepository : IFormatoComisionRepository
    {
        private readonly ApplicationDbContext _context;

        public FormatoComisionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FormatoComisionDto> GetFormatoComisionByOficinaEjercicioNoviat(int oficina, int ejercicio, int noViat)
        {
            var result = await _context.FormatoComisionDto.Where(x => x.Oficina == oficina && x.Ejercicio == ejercicio && x.NoViat == noViat).FirstOrDefaultAsync();
            return result;

        }
    }
}
