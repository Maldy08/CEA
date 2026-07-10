using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Domain.Entities.IndiArct;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.IndiArct
{
    public class IndiArctCatpresasRepository : IIndiArctCatpresasRepository
    {
        private readonly ApplicationDbContext _context;

        public IndiArctCatpresasRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IndiArctCatpresasDto>> GetAllAsync()
            => await _context.IndiArctCatpresas
                .Select(e => new IndiArctCatpresasDto
                {
                    Id = e.Id,
                    NombreOficial = e.NombreOficial,
                    Municipio = e.Municipio,
                    Latitud = e.Latitud,
                    Longitud = e.Longitud,
                    CorrientePrincipal = e.CorrientePrincipal,
                    CapacidadNamoHm3 = e.CapacidadNamoHm3,
                    UsoPrincipal = e.UsoPrincipal
                })
                .OrderBy(e => e.Id)
                .ToListAsync();

        public async Task<IndiArctCatpresas?> GetByIdAsync(int id)
            => await _context.IndiArctCatpresas.FindAsync(id);

        public async Task<IndiArctCatpresas> AddAsync(IndiArctCatpresas entity)
        {
            await _context.IndiArctCatpresas.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(IndiArctCatpresas entity)
        {
            var existing = await _context.IndiArctCatpresas.FindAsync(entity.Id);
            if (existing == null) return;
            existing.NombreOficial = entity.NombreOficial;
            existing.Municipio = entity.Municipio;
            existing.Latitud = entity.Latitud;
            existing.Longitud = entity.Longitud;
            existing.CorrientePrincipal = entity.CorrientePrincipal;
            existing.CapacidadNamoHm3 = entity.CapacidadNamoHm3;
            existing.UsoPrincipal = entity.UsoPrincipal;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IndiArctCatpresas.FindAsync(id);
            if (entity == null) return;
            _context.IndiArctCatpresas.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
