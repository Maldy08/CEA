using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Domain.Entities.IndiArct;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.IndiArct
{
    public class IndiArctPresasNivelesRepository : IIndiArctPresasNivelesRepository
    {
        private readonly ApplicationDbContext _context;

        public IndiArctPresasNivelesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IndiArctPresasNivelesDto>> GetAllAsync()
            => await _context.IndiArctPresasNiveles
                .Select(e => new IndiArctPresasNivelesDto
                {
                    Id = e.Id,
                    Anio = e.Anio,
                    Mes = e.Mes,
                    IdPresa = e.IdPresa,
                    VolumenM3 = e.VolumenM3,
                    NombrePresa = _context.IndiArctCatpresas
                        .Where(p => p.Id == e.IdPresa)
                        .Select(p => p.NombreOficial)
                        .FirstOrDefault()
                })
                .OrderBy(e => e.IdPresa).ThenBy(e => e.Anio).ThenBy(e => e.Mes)
                .ToListAsync();

        public async Task<IEnumerable<IndiArctPresasNivelesDto>> GetByPresaAsync(int idPresa)
            => await _context.IndiArctPresasNiveles
                .Where(e => e.IdPresa == idPresa)
                .Select(e => new IndiArctPresasNivelesDto
                {
                    Id = e.Id,
                    Anio = e.Anio,
                    Mes = e.Mes,
                    IdPresa = e.IdPresa,
                    VolumenM3 = e.VolumenM3,
                    NombrePresa = _context.IndiArctCatpresas
                        .Where(p => p.Id == e.IdPresa)
                        .Select(p => p.NombreOficial)
                        .FirstOrDefault()
                })
                .OrderBy(e => e.Anio).ThenBy(e => e.Mes)
                .ToListAsync();

        public async Task<IEnumerable<IndiArctPresasNivelesDto>> GetByAnioAsync(int anio)
            => await _context.IndiArctPresasNiveles
                .Where(e => e.Anio == anio)
                .Select(e => new IndiArctPresasNivelesDto
                {
                    Id = e.Id,
                    Anio = e.Anio,
                    Mes = e.Mes,
                    IdPresa = e.IdPresa,
                    VolumenM3 = e.VolumenM3,
                    NombrePresa = _context.IndiArctCatpresas
                        .Where(p => p.Id == e.IdPresa)
                        .Select(p => p.NombreOficial)
                        .FirstOrDefault()
                })
                .OrderBy(e => e.IdPresa).ThenBy(e => e.Mes)
                .ToListAsync();

        public async Task<IEnumerable<PresaCapturaDto>> GetGridAsync(int anio, int mes)
            => await (from p in _context.IndiArctCatpresas
                      join n in _context.IndiArctPresasNiveles
                            .Where(x => x.Anio == anio && x.Mes == mes)
                           on p.Id equals n.IdPresa into capturas
                      from n in capturas.DefaultIfEmpty()
                      orderby p.Id
                      select new PresaCapturaDto
                      {
                          IdPresa = p.Id,
                          NombreOficial = p.NombreOficial,
                          Municipio = p.Municipio,
                          IdCaptura = n != null ? (int?)n.Id : null,
                          VolumenM3 = n != null ? (decimal?)n.VolumenM3 : null
                      }).ToListAsync();

        public async Task UpsertLoteAsync(int anio, int mes, IEnumerable<PresaVolumenItem> items)
        {
            foreach (var item in items)
            {
                var existing = await _context.IndiArctPresasNiveles
                    .FirstOrDefaultAsync(x => x.Anio == anio && x.Mes == mes && x.IdPresa == item.IdPresa);

                if (existing == null)
                {
                    await _context.IndiArctPresasNiveles.AddAsync(new IndiArctPresasNiveles
                    {
                        Anio = anio,
                        Mes = mes,
                        IdPresa = item.IdPresa,
                        VolumenM3 = item.VolumenM3
                    });
                }
                else
                {
                    existing.VolumenM3 = item.VolumenM3;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IndiArctPresasNiveles?> GetByIdAsync(int id)
            => await _context.IndiArctPresasNiveles.FindAsync(id);

        public async Task<IndiArctPresasNiveles> AddAsync(IndiArctPresasNiveles entity)
        {
            await _context.IndiArctPresasNiveles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(IndiArctPresasNiveles entity)
        {
            var existing = await _context.IndiArctPresasNiveles.FindAsync(entity.Id);
            if (existing == null) return;
            existing.Anio = entity.Anio;
            existing.Mes = entity.Mes;
            existing.IdPresa = entity.IdPresa;
            existing.VolumenM3 = entity.VolumenM3;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IndiArctPresasNiveles.FindAsync(id);
            if (entity == null) return;
            _context.IndiArctPresasNiveles.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
