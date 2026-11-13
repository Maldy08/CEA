using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Nomina
{
    public class NomVacacionesFunctions : INomVacacionesFunctions
    {
        private readonly ApplicationDbContext _context;

        public NomVacacionesFunctions(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SimulaVacacionesDto>> SimulaVacaciones(int noEmp, int dias)
        {
            return await _context.SimulaVacacionesDtos
                .FromSqlInterpolated($"SELECT * FROM TABLE (F_SIMULA_VACACIONES({noEmp},{dias}))")
                .ToListAsync();
        }

        public async Task<SaldoVacacionesDto?> SaldoVacaciones(int noEmp)
        {
            // Un renglón por empleado activo; ninguno si no existe o está inactivo.
            var saldos = await _context.SaldoVacacionesDtos
                .FromSqlInterpolated($"SELECT * FROM TABLE (F_SALDO_VACACIONES({noEmp}))")
                .ToListAsync();
            return saldos.FirstOrDefault();
        }

        // Renglones de FN_CONSULTA_VACACIONES en el orden en que la función los emite (ver ConsultaVacacionesRenglonDto).
        public async Task<List<string?>> ConsultaVacaciones(int noEmp, DateTime desde, DateTime hasta)
        {
            return await _context.ConsultaVacacionesRenglonDtos
                .FromSqlInterpolated($"SELECT ROWNUM AS ORDEN, VALOR FROM TABLE (FN_CONSULTA_VACACIONES({desde.Date},{hasta.Date},{noEmp}))")
                .OrderBy(x => x.Orden)
                .Select(x => x.Valor)
                .ToListAsync();
        }
    }
}
