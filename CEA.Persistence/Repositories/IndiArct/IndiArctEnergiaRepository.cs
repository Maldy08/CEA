using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Domain.Entities.IndiArct;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.IndiArct
{
    public class IndiArctEnergiaRepository : IIndiArctEnergiaRepository
    {
        private readonly ApplicationDbContext _context;

        public IndiArctEnergiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IndiArctEnergiaDto>> GetAllAsync()
            => await _context.IndiArctEnergia
                .Select(e => new IndiArctEnergiaDto
                {
                    Id = e.Id,
                    Anio = e.Anio,
                    Mes = e.Mes,
                    Volumenes = e.Volumenes,
                    Kwh = e.Kwh,
                    Costo = e.Costo
                })
                .OrderBy(e => e.Anio).ThenBy(e => e.Mes)
                .ToListAsync();

        public async Task<IEnumerable<EnergiaCapturaDto>> GetGridAsync(int anio)
        {
            var existentes = (await _context.IndiArctEnergia
                    .Where(e => e.Anio == anio)
                    .ToListAsync())
                .GroupBy(e => e.Mes)
                .ToDictionary(g => g.Key, g => g.First());

            var grid = new List<EnergiaCapturaDto>();
            for (int mes = 1; mes <= 12; mes++)
            {
                existentes.TryGetValue(mes, out var e);
                grid.Add(new EnergiaCapturaDto
                {
                    Mes = mes,
                    IdEnergia = e?.Id,
                    Volumenes = e?.Volumenes,
                    Kwh = e?.Kwh,
                    Costo = e?.Costo
                });
            }
            return grid;
        }

        public async Task UpsertLoteAsync(int anio, IEnumerable<EnergiaMesItem> items)
        {
            foreach (var item in items)
            {
                var existing = await _context.IndiArctEnergia
                    .FirstOrDefaultAsync(e => e.Anio == anio && e.Mes == item.Mes);

                if (existing == null)
                {
                    await _context.IndiArctEnergia.AddAsync(new IndiArctEnergia
                    {
                        Anio = anio,
                        Mes = item.Mes,
                        Volumenes = item.Volumenes,
                        Kwh = item.Kwh,
                        Costo = item.Costo
                    });
                }
                else
                {
                    existing.Volumenes = item.Volumenes;
                    existing.Kwh = item.Kwh;
                    existing.Costo = item.Costo;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IndiArctEnergia?> GetByIdAsync(int id)
            => await _context.IndiArctEnergia.FindAsync(id);

        public async Task<IndiArctEnergia> AddAsync(IndiArctEnergia entity)
        {
            await _context.IndiArctEnergia.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(IndiArctEnergia entity)
        {
            var existing = await _context.IndiArctEnergia.FindAsync(entity.Id);
            if (existing == null) return;
            existing.Anio = entity.Anio;
            existing.Mes = entity.Mes;
            existing.Volumenes = entity.Volumenes;
            existing.Kwh = entity.Kwh;
            existing.Costo = entity.Costo;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IndiArctEnergia.FindAsync(id);
            if (entity == null) return;
            _context.IndiArctEnergia.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
