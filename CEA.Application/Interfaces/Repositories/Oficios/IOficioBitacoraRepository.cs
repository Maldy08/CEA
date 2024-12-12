
using CEA.Application.DTOs.Oficios;


namespace CEA.Application.Interfaces.Repositories.Oficios
{
    public interface IOficioBitacoraRepository
    {
        Task<List<OficioBitacoraDto>> GetOficioBitacoraByEjercicioFolioEor(int ejercicio, int folio, int eor);
    }
}
