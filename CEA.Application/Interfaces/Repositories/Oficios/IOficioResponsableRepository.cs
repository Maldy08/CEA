
using CEA.Application.DTOs.Oficios;

namespace CEA.Application.Interfaces.Repositories.Oficios
{
    public interface IOficioResponsableRepository
    {
        Task<List<OficioResponsableDto>> GetOficioReponsableByEjercicioFolioEor( int ejercicio, int folio, int eor);
        Task<List<OficioResponsableDto>> GetOficioReponsableByEjercicioFolioEor(int ejercicio, int folio, int eor, int rol);
        Task<OficioResponsableDto> GetOficioResponsableByEjercicioFolioEorIdEmpleadoRol(int ejercicio, int folio, int eor, int idEmpleado, int rol);


    }
}
