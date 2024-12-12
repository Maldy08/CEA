
using CEA.Application.DTOs.Oficios;

namespace CEA.Application.Interfaces.Repositories.Oficios
{
    public interface IOficioEstatusRepository
    {
        Task<OficioEstatusDto> GetEstatusByIdEor(int id, int eor);
        Task<List<OficioEstatusDto>> GetAll();
    }
}
