using CEA.Application.DTOs.Oficios;

namespace CEA.Application.Interfaces.Repositories.Oficios
{
    public interface IOficioGpiRepository
    {
        public Task<IEnumerable<OficioGpiDto>> GetAllOficioGpi();
    }
}
