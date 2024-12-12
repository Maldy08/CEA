

using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Oficios
{
    public class OficioResponsableRepository : IOficioResponsableRepository

    {

        private readonly ApplicationDbContext _context;

        public OficioResponsableRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OficioResponsableDto>> GetOficioReponsableByEjercicioFolioEor(int ejercicio, int folio, int eor)
        {

            var oficios = await _context.OficioResponsable.Where(x =>  x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor)
                .Select(x => new OficioResponsableDto
                {
                    Ejercicio = x.Ejercicio,
                    Folio = x.Folio,
                    Eor = x.Eor,
                    IdEmpleado = x.IdEmpleado,
                    Rol = x.Rol,

                }).OrderBy(x => x.Ejercicio).ThenBy(x => x.Folio).ThenBy(x => x.Eor).ToListAsync();

            return await _context.OficioResponsable.Where(x => x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor)
                .Select(x => new OficioResponsableDto
                {
                    Ejercicio = x.Ejercicio,
                    Folio = x.Folio,
                    Eor = x.Eor,
                    IdEmpleado = x.IdEmpleado,
                    Rol = x.Rol,

                }).OrderBy(x => x.Ejercicio).ThenBy(x => x.Folio).ThenBy(x => x.Eor).ToListAsync();
        }
    }
}
