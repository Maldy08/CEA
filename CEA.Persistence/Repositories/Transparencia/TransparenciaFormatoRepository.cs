using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Transparencia
{
    public class TransparenciaFormatoRepository : ITransparenciaFormatoRepository
    {
        private readonly ApplicationDbContextSQL _context;

        public TransparenciaFormatoRepository(ApplicationDbContextSQL context)
        {
            _context = context;
        }



        public async Task<List<GetFormatoByUserIdDto>> GetFormatoByUserId(int id)
        {
            var result = await _context.Usuarios
                  .Where(x => x.IdUsuario == id)
                  .Include(dep => dep.IdDeptoNavigation.AccesoReportes)
                  .ThenInclude(rep => rep.Reporte)
                  .Select(a => new GetFormatoByUserIdDto
                  {
                      IdDepto = a.IdDepto,
                      Reporte = a.IdDeptoNavigation.AccesoReportes.Select(b => new ReporteDto
                      {
                          Nombre = b.Reporte.Nombre,
                          Codigo = b.Reporte.Codigo
                      }).ToList()
                  }).ToListAsync();

            return result;

        }

        public async Task<GetNombreFormatoDto> GetNombreFormatoByUserId(string nombreFormato)
        {
            return await _context.Reporte.Where(x => x.Codigo == nombreFormato)
                 .Select(a => new GetNombreFormatoDto
                 {
                     Nombre = a.Nombre
                 }).FirstOrDefaultAsync();

        }

        public async Task<List<GetBitacorasByUserIdDto>> GetBitacorasByUserId(int id)
        {
            return await _context.BitacoraArchivo
                 .Where(b => b.IdUsuario == id)
                 .Select(a => new GetBitacorasByUserIdDto
                 {
                     Id = a.IdBitacora,
                     NombreReporte = a.NombreReporte,
                     Nombre = a.NombreArchivo,
                     Hipervinculo = a.Hipervinculo != null ? a.Hipervinculo : "",
                 }).OrderBy(a => a.Nombre)
                 .ToListAsync();
        }
    }
}
