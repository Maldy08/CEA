using CEA.Application.DTOs.Bitacora;
using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Domain.Entities.Bitacora;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Bitacora
{
    public class TemaRepository : ITemaRepository
    {
        private readonly ApplicationDbContext _context;

        public TemaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tema>> GetAllAsync()
            => await _context.Temas.ToListAsync();

        public async Task<IEnumerable<TemaDto>> GetAllConContadoresAsync()
            => await _context.Temas
                .Select(t => new TemaDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    FechaCreacion = t.FechaCreacion,
                    FechaLimite = t.FechaLimite,
                    IdDepartamentoOrigen = t.IdDepartamentoOrigen,
                    IdCreador = t.IdCreador,
                    NombreDepartamento = _context.DeptoUe
                        .Where(d => d.IdCea == t.IdDepartamentoOrigen)
                        .Select(d => d.Descripcion)
                        .FirstOrDefault(),
                    TotalAvances = _context.Avances.Count(a => a.IdTema == t.Id),
                    UltimoAvance = _context.Avances
                        .Where(a => a.IdTema == t.Id)
                        .Max(a => (DateTime?)a.FechaHora)
                })
                .ToListAsync();

        public async Task<Tema?> GetByIdAsync(int id)
            => await _context.Temas.FindAsync(id);

        public async Task<IEnumerable<Tema>> GetByUsuarioAsync(int idUsuario)
            => await _context.TemaInvolucrados
                .Where(ti => ti.IdUsuario == idUsuario)
                .Join(_context.Temas, ti => ti.IdTema, t => t.Id, (ti, t) => t)
                .ToListAsync();

        public async Task<IEnumerable<TemaDto>> GetByUsuarioConContadoresAsync(int idUsuario)
            => await _context.TemaInvolucrados
                .Where(ti => ti.IdUsuario == idUsuario)
                .Join(_context.Temas, ti => ti.IdTema, t => t.Id, (ti, t) => t)
                .Select(t => new TemaDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    FechaCreacion = t.FechaCreacion,
                    FechaLimite = t.FechaLimite,
                    IdDepartamentoOrigen = t.IdDepartamentoOrigen,
                    IdCreador = t.IdCreador,
                    NombreDepartamento = _context.DeptoUe
                        .Where(d => d.IdCea == t.IdDepartamentoOrigen)
                        .Select(d => d.Descripcion)
                        .FirstOrDefault(),
                    TotalAvances = _context.Avances.Count(a => a.IdTema == t.Id),
                    UltimoAvance = _context.Avances
                        .Where(a => a.IdTema == t.Id)
                        .Max(a => (DateTime?)a.FechaHora)
                })
                .ToListAsync();

        public async Task<Tema> AddAsync(Tema tema)
        {
            await _context.Temas.AddAsync(tema);
            await _context.SaveChangesAsync();
            return tema;
        }

        public async Task UpdateAsync(Tema tema)
        {
            var existing = await _context.Temas.FindAsync(tema.Id);
            if (existing == null) return;
            existing.Titulo = tema.Titulo;
            existing.Descripcion = tema.Descripcion;
            existing.Estado = tema.Estado;
            existing.FechaLimite = tema.FechaLimite;
            existing.IdDepartamentoOrigen = tema.IdDepartamentoOrigen;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tema = await _context.Temas.FindAsync(id);
            if (tema == null) return;
            _context.Temas.Remove(tema);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEstadoAsync(int id, string estado)
        {
            var tema = await _context.Temas.FindAsync(id);
            if (tema == null) return;
            tema.Estado = estado;
            await _context.SaveChangesAsync();
        }
    }
}
