

using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;

using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Oficios
{
    public class OficioBitacoraRepository : IOficioBitacoraRepository
    {
        private readonly ApplicationDbContext _context;

        public OficioBitacoraRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OficioBitacoraDto>> GetOficioBitacoraByEjercicioFolioEor(int ejercicio, int folio, int eor)
        {
            return await _context.OficioBitacora
                .Where(x => x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor)
                .Select(x => new OficioBitacoraDto
                {
              
                    Ejercicio = x.Ejercicio,
                    Folio = x.Folio,
                    Eor = x.Eor,
                    FechaCaptura = x.FechaCaptura,
                    IdEmpleado = x.IdEmpleado,
                    Estatus = x.Estatus,
                    Comentarios = x.Comentarios
                })
                .ToListAsync();
        }
    }
}
