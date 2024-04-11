using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace CEA.Persistence.Repositories
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
