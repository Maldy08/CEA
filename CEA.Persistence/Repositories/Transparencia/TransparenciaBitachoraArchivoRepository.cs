using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Transparencia
{
    public class TransparenciaBitachoraArchivoRepository : ITransparenciaBitachoraArchivoRepository
    {
        private readonly ApplicationDbContextSQL _context;

        public TransparenciaBitachoraArchivoRepository(ApplicationDbContextSQL context)
        {
            _context = context;
        }
        public async Task<List<GetBitacorasByUserIdDto>> GetBitacorasByUserId(int id)
        {
            return await _context.BitacoraArchivo
                .Where(a => a.IdUsuario == id)
                .Select(a => new GetBitacorasByUserIdDto
                {
                    Id = a.IdBitacora,
                    Hipervinculo = a.Hipervinculo != null ? a.Hipervinculo : "",
                    Nombre = a.NombreArchivo,
                    NombreReporte = a.NombreReporte,
                }).ToListAsync();
        }

        public async Task<List<GetBitacorasByUserIdDto>> GetBitacorasByUserIdAndFormato(int id, string formato)
        {
            return await _context.BitacoraArchivo
                .Where(a => a.IdUsuario == id)
                .Where(a => a.NombreReporte == formato)
                .Select(a => new GetBitacorasByUserIdDto
                {
                    Id = a.IdBitacora,
                    Hipervinculo = a.Hipervinculo != null ? a.Hipervinculo : "",
                    Nombre = a.NombreArchivo,
                    NombreReporte = a.NombreReporte,
                }).ToListAsync();
        }
    }
}
