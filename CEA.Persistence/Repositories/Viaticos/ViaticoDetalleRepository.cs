

using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticoDetalleRepository : IViaticoDetalleRepository
    {
        private readonly ApplicationDbContext _context;

        public ViaticoDetalleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ViaticoDetalleDto> GetAllViaticosByEjercicioAndNoviatAndOficina(int ejercicio, int noviat, int oficina)
        {
            return  await _context.ViaticoDetalleDto.FromSqlInterpolated($"select * from table (F_DETALLEVIATICO({ejercicio},{noviat},{oficina}))").FirstOrDefaultAsync();
        }
    }
}
