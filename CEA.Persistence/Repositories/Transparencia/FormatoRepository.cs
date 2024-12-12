

using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Transparencia
{
    public class FormatoRepository : IFormatoRepository
    {

        private readonly ApplicationDbContextSQL _context;

        public FormatoRepository(ApplicationDbContextSQL context)
        {
            _context = context;
        }

        public async Task<List<FormatoDto>> GetFormatos()
        {
            return await _context.Reporte.Select(a => new FormatoDto
            {
                Codigo = a.Codigo,
                Descripcion = a.Descripcion,
                IdAnexo = a.IdAnexo,
                IdAnexoInciso = a.IdAnexoInciso,
                IdArticulo = a.IdArticulo,
                Nombre = a.Nombre,
                Periocidad = a.Periocidad,

            }).OrderBy(a => a.IdArticulo).ThenBy(a => a.IdAnexo).ThenBy(a => a.IdAnexoInciso).ToListAsync();

        }

        public async Task<List<FormatoDto>> GetFormatosByIdUser(int userId)
        {
            return await _context.Reporte.Select(a => new FormatoDto
            {
                Codigo = a.Codigo,
                Descripcion = a.Descripcion,
                IdAnexo = a.IdAnexo,
                IdAnexoInciso = a.IdAnexoInciso,
                IdArticulo = a.IdArticulo,
                Nombre = a.Nombre,
                Periocidad = a.Periocidad,

            })
                // .Where(a => a.IdArticulo == userId)
                .OrderBy(a => a.IdArticulo).ThenBy(a => a.IdAnexo).ThenBy(a => a.IdAnexoInciso).ToListAsync();
        }
    }
}
