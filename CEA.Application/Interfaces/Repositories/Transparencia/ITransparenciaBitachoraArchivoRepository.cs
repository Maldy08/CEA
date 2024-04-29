
using CEA.Application.DTOs.Transparencia;

namespace CEA.Application.Interfaces.Repositories.Transparencia
{
    public interface ITransparenciaBitachoraArchivoRepository
    {
        Task<List<GetBitacorasByUserIdDto>> GetBitacorasByUserId(int id);
        Task<List<GetBitacorasByUserIdDto>> GetBitacorasByUserIdAndFormato(int id, string formato);
        
    }
}
