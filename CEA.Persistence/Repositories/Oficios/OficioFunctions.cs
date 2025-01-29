using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Oficios
{
    public class OficioFunctions : IOficioFunctions
    {
        private readonly ApplicationDbContext _context;

        public OficioFunctions(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OficioContadoresDashboardDto>> GetContadoresDashboard(int ejercicio, int idEmpleado)
        {
            return await _context.oficioContadoresDashboardDtos.FromSqlInterpolated($"SELECT * FROM TABLE (F_CONTADORES_DASHBOARD1({ejercicio},{idEmpleado}))").ToListAsync();
        }

        public async Task<List<OficioListaDashboardDto>> GetListaDashboard(int ejercicio, int idEmpleado)
        {
            var entities = await _context.oficioListaDashboardDtos.FromSqlInterpolated($"SELECT * FROM TABLE (F_LISTA_DASHBOARD1({ejercicio},{idEmpleado}))").ToListAsync();

            return await _context.oficioListaDashboardDtos.FromSqlInterpolated($"SELECT * FROM TABLE (F_LISTA_DASHBOARD1({ejercicio},{idEmpleado}))").ToListAsync();
        }

        public async Task<List<OficioDtoFunction>> GetListadoOficioFunction(int ejercicio, int eor, int idEmpleado)
        {
            return await _context.OficioDtoFunction.FromSqlInterpolated($"SELECT * FROM TABLE (F_LISTADOOFICIOS({ejercicio},{eor},{idEmpleado}))").ToListAsync();
        }
    }
}
