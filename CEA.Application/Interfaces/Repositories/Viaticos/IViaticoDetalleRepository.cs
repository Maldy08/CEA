

using CEA.Application.DTOs.Viaticos;

namespace CEA.Application.Interfaces.Repositories.Viaticos
{
    public interface IViaticoDetalleRepository
    {
      
        Task<ViaticoDetalleDto> GetAllViaticosByEjercicioAndNoviatAndOficina(int ejercicio, int noviat, int oficina);
    }
}
