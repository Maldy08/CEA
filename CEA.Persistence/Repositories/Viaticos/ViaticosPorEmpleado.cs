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

        public async Task<List<ViaticoCapturadoDto>> GetCapturadosAsync(int ejercicio, int noEmpCrea)
        {
            return await (from v in _context.Viatico.AsNoTracking()
                          join e in _context.Empleado.AsNoTracking() on v.NoEmp equals e.IdEmpleado into emp
                          from e in emp.DefaultIfEmpty()
                          where v.Ejercicio == ejercicio && v.NoEmpCrea == noEmpCrea && v.NoEmp != noEmpCrea
                          select new ViaticoCapturadoDto
                          {
                              Oficina = v.Oficina,
                              Ejercicio = v.Ejercicio,
                              NoViat = v.NoViat,
                              NoEmp = v.NoEmp,
                              NombreEmpleado = e != null ? e.NombreCompleto : null
                          }).ToListAsync();
        }

    }
}
