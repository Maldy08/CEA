

using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Oficios
{
    public class OficioUsuExtRepository : IOficioUsuExtRepository
    {
        private readonly ApplicationDbContext _context;

        public OficioUsuExtRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OficioUsuExtDto>> GetOficiosUsuariosExternos()
        {
            return await _context.OficioUsuExtDto
                .Select(o => new OficioUsuExtDto
                {
                    IdExterno = o.IdExterno,
                    Frecuencia = o.Frecuencia,
                    Empresa = o.Empresa,
                    Siglas = o.Siglas,
                    Nombre = o.Nombre,
                    Cargo = o.Cargo,
       
                })
                .ToListAsync();
        }
    }
}
