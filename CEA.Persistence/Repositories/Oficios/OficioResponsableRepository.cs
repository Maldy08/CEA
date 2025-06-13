

using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Domain.Entities.Oficios;
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

            var x = 1;


            //var oficios = await _context.OficioResponsable.Where(x => x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor)
            //    .Select(x => new OficioResponsableDto
            //    {
            //        Id = x.Id,
            //        Ejercicio = x.Ejercicio,
            //        Folio = x.Folio,
            //        Eor = x.Eor,
            //        IdEmpleado = x.IdEmpleado,
            //        Rol = x.Rol,

            //    }).OrderBy(x => x.Ejercicio).ThenBy(x => x.Folio).ThenBy(x => x.Eor).ToListAsync();

            return await _context.OficioResponsable.Where(x => x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor)
                .Select(x => new OficioResponsableDto
                {
                    Id = x.Id,
                    Ejercicio = x.Ejercicio,
                    Folio = x.Folio,
                    Eor = x.Eor,
                    IdEmpleado = x.IdEmpleado,
                    Rol = x.Rol,
                    Iox = x.Iox,


                }).OrderBy(x => x.Ejercicio).ThenBy(x => x.Folio).ThenBy(x => x.Eor).ToListAsync();
        }

        public async Task<List<OficioResponsableDto>> GetOficioReponsableByEjercicioFolioEor(int ejercicio, int folio, int eor, int rol)
        {
            var entities = await _context.OficioResponsable.Where(x => x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor && x.Rol == rol)
                .Select(x => new OficioResponsableDto
                {
                    Id = x.Id,
                    Ejercicio = x.Ejercicio,
                    Folio = x.Folio,
                    Eor = x.Eor,
                    IdEmpleado = x.IdEmpleado,
                    Rol = x.Rol,
                    Iox = x.Iox,
                }).OrderBy(x => x.Ejercicio).ThenBy(x => x.Folio).ThenBy(x => x.Eor).ToListAsync(); 


            return await _context.OficioResponsable.Where(x => x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor && x.Rol == rol)
                .Select(x => new OficioResponsableDto
                {
                    Id = x.Id,
                    Ejercicio = x.Ejercicio,
                    Folio = x.Folio,
                    Eor = x.Eor,
                    IdEmpleado = x.IdEmpleado,
                    Rol = x.Rol,
                    Iox = x.Iox,

                }).OrderBy(x => x.Ejercicio).ThenBy(x => x.Folio).ThenBy(x => x.Eor).ToListAsync();
        }

        public async Task<OficioResponsable> GetOficioReponsableByEjercicioFolioEorNoDto(int ejercicio, int folio, int eor, int idEmpleado, int rol)
        {
            return await _context.OficioResponsable.Where(x => x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor && x.IdEmpleado == idEmpleado && x.Rol == rol)
                .FirstOrDefaultAsync();
        }

        public async Task<OficioResponsableDto> GetOficioResponsableByEjercicioFolioEorIdEmpleadoRol(int ejercicio, int folio, int eor, int idEmpleado, int rol)
        {
            return await _context.OficioResponsable.Where(x => x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor && x.IdEmpleado == idEmpleado && x.Rol == rol)
                .Select(x => new OficioResponsableDto
                {

                    Ejercicio = x.Ejercicio,
                    Folio = x.Folio,
                    Eor = x.Eor,
                    IdEmpleado = x.IdEmpleado,
                    Rol = x.Rol,
                    Id = x.Id,
                    Iox = x.Iox,

                }).FirstOrDefaultAsync();
        }
    }
}
