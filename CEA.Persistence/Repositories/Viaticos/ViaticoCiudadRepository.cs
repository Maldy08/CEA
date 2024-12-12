using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticoCiudadRepository : IViaticoCiudadRepository
    {

        private readonly ApplicationDbContext _context;

        public ViaticoCiudadRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ViaticoCiudadDto>> GetAll()
        {
            return await _context.ViaticoCiudad
                .Select(a => new ViaticoCiudadDto
                {
                    IdCiudad = a.IdCiudad,
                    IdEstado = a.IdEstado,
                    Ciudad = a.Ciudad,
             
                }).OrderBy(a => a.IdCiudad).ToListAsync();
           
        }

        public async Task<ViaticoCiudadDto> GetById(int id)
        {
           return await _context.ViaticoCiudad
                .Select(a => new ViaticoCiudadDto
                {
                    IdCiudad = a.IdCiudad,
                    IdEstado = a.IdEstado,
                    Ciudad = a.Ciudad,
             
                }).FirstOrDefaultAsync(a => a.IdCiudad == id);
        }

        public async Task<List<ViaticoCiudadDto>> GetByIdEstado(int id)
        {
            return await _context.ViaticoCiudad
                .Select(a => new ViaticoCiudadDto
                {
                    IdCiudad = a.IdCiudad,
                    IdEstado = a.IdEstado,
                    Ciudad = a.Ciudad,
             
                }).Where(a => a.IdEstado == id).OrderBy(a => a.IdCiudad).ToListAsync();
        }
    }
}
