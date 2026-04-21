
using CEA.Application.DTOs.Oficios;
using CEA.Domain.Entities.Oficios;

namespace CEA.Application.Interfaces.Repositories.Oficios
{
    public interface IOficioResponsableRepository
    {
        Task<List<OficioResponsableDto>> GetOficioReponsableByEjercicioFolioEor( int ejercicio, int folio, int eor);
        Task<List<OficioResponsableDto>> GetOficioReponsableByEjercicioFolioEor(int ejercicio, int folio, int eor, int rol);
        Task<OficioResponsableDto> GetOficioResponsableByEjercicioFolioEorIdEmpleadoRol(int ejercicio, int folio, int eor, int idEmpleado, int rol);
        Task<OficioResponsable> GetOficioReponsableByEjercicioFolioEorNoDto(int ejercicio, int folio, int eor, int idEmpleado, int rol);
        Task<bool> TienePermisoParaVerOficio(int ejercicio, int folio, int eor, int idEmpleado);

    }
}
