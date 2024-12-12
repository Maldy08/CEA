using CEA.Application.DTOs.Oficios;
using CEA.Domain.Entities.Oficios;

namespace CEA.Application.Interfaces.Repositories.Oficios
{
    public interface IOficioRepository
    {
        Task<List<OficioDto>> GetAllOficios();
        Task<List<OficioDto>> GetOficiosMC(int eor);
        Task<List<OficioDto>> GetOficiosUsuarios(int ejercicio, int eor, int idEmpleado, int idDepto);
        Task<OficioDto> GetOficioByFolio(int ejercicio, int eor, int folio);
        Task<List<Oficio>> GetOficiosMCByEjercicio(int eor, int ejercicio);
        Task<Oficio> GetOficio(int ejercicio, int folio, int eor);
    }
}
