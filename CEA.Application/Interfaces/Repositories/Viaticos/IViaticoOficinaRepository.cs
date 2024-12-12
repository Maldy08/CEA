using CEA.Application.DTOs.Viaticos;

namespace CEA.Application.Interfaces.Repositories.Viaticos
{
    public interface IViaticoOficinaRepository
    {
        Task<List<ViaticoOficinaDto>> GetAll();

    }
}
