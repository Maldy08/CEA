using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticosPorEmpleado : IViaticoPorEmpleadoDto
    {
        private readonly ApplicationDbContext _context;

        public ViaticosPorEmpleado(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<List<ViaticosPorEmpleadoDto>> GetListadoPorEjercicioAndEmpleado(int ejercicio, int empleado)
        {
            return _context.ViaticosPorEmpleadosDto.FromSqlInterpolated($"select * from table (F_LISTAVIATICOSXEMP({ejercicio},{empleado}))").ToListAsync();
        }

    }
}
