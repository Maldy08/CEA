using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Nomina
{
    public class NomDiasFestivosRepository : INomDiasFestivosRepository
    {
        private readonly ApplicationDbContext _context;

        public NomDiasFestivosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Festivos activos del rango que le tocan al empleado según su tipo (ID_TIPOEMP: B base, C confianza,
        // T contrato), el sexo de ese tipo y su oficina (LUGARTRAB; festivo sin oficinas = todas). Se lee de
        // EMPLEADOS porque VS_EMPLEADOS no trae SEXO ni ID_TIPOEMP. *_REQ_HIJOS todavía no se evalúa: no hay
        // dato de hijos en Oracle. Si el empleado no existe no sale nada.
        public async Task<List<NomDiasFestivosDto>> GetByEmpleadoAsync(int noEmp, DateTime desde, DateTime hasta)
        {
            return await _context.NomDiasFestivos
                .FromSqlInterpolated($@"SELECT F.* FROM NOM_DIAS_FESTIVOS F
                    JOIN EMPLEADOS E ON E.EMPLEADO = {noEmp}
                    WHERE F.ACTIVO = 1
                      AND F.FECHA_FESTIVO BETWEEN TRUNC({desde}) AND TRUNC({hasta})
                      AND (   (TRIM(E.ID_TIPOEMP) = 'B' AND F.APLICA_BASE = 1 AND (F.BASE_SEXO IS NULL OR F.BASE_SEXO = E.SEXO))
                           OR (TRIM(E.ID_TIPOEMP) = 'C' AND F.APLICA_CONFIANZA = 1 AND (F.CONF_SEXO IS NULL OR F.CONF_SEXO = E.SEXO))
                           OR (TRIM(E.ID_TIPOEMP) = 'T' AND F.APLICA_CONTRATO = 1 AND (F.CONT_SEXO IS NULL OR F.CONT_SEXO = E.SEXO)))
                      AND (NOT EXISTS (SELECT 1 FROM NOM_DIAS_FESTIVOS_OFICINAS O WHERE O.ID_FESTIVO = F.ID_FESTIVO)
                           OR EXISTS (SELECT 1 FROM NOM_DIAS_FESTIVOS_OFICINAS O
                                      WHERE O.ID_FESTIVO = F.ID_FESTIVO AND O.ID_OFICINA = E.LUGARTRAB))")
                .OrderBy(x => x.FechaFestivo)
                .Select(x => new NomDiasFestivosDto
                {
                    IdFestivo = x.IdFestivo,
                    FechaFestivo = x.FechaFestivo,
                    Descripcion = x.Descripcion,
                    FechaOficial = x.FechaOficial,
                    HoraEntrada = x.HoraEntrada,
                    HoraSalida = x.HoraSalida
                })
                .ToListAsync();
        }
    }
}
