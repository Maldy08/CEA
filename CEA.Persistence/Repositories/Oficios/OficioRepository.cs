using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Domain.Entities.Oficios;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Oficios
{
    public class OficioRepository : IOficioRepository
    {

        private readonly ApplicationDbContext _context;

        public OficioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OficioDto>> GetAllOficios()
        {
            return await _context.OficioDto.ToListAsync();
        }

        public async Task<OficioDto> GetOficioByFolio(int ejercicio, int eor, int folio)
        {
            return await _context.OficioDto.Where(x => x.Eor == eor && x.Folio == folio && x.Ejercicio == ejercicio ).FirstOrDefaultAsync();
        }

        public async Task<List<OficioDto>> GetOficiosMC(int eor)
        {
            return await _context.OficioDto.Where(x => x.Eor == eor).ToListAsync();
        }



        public async Task<List<OficioDto>> GetOficiosUsuarios(int ejercicio, int eor, int idEmpleado, int idDepto)
        {
           return await _context.OficioDto.Where(x => x.Ejercicio == ejercicio && x.Eor == eor && x.IdEmpleado == idEmpleado && x.Depto == idDepto).ToListAsync();
        }

        public  async Task<List<Oficio>> GetOficiosMCByEjercicio(int eor, int ejercicio)
        {
            return await _context.Oficio.Where(x => x.Eor == eor && x.Ejercicio == ejercicio).ToListAsync();
        }

        public async Task<Oficio> GetOficio(int ejercicio, int folio, int eor)
        {
           return await _context.Oficio.Where(x => x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor).FirstOrDefaultAsync();
        }
    }
}
