
using CEA.Application.DTOs.Oficios;


namespace CEA.Application.Interfaces.Repositories.Oficios
{
    public interface IOficioUsuExtRepository
    {
        Task<List<OficioUsuExtDto>> GetOficiosUsuariosExternos();
        Task<List<OficioUsuExtDto>> GetOficiosUsuariosExternosMantenimiento();


    }
}
