using CEA.Application.DTOs.Viaticos;

namespace CEA.Application.Interfaces.Repositories.Viaticos
{
    public interface IViaticoPaisRepository
    {
        Task<List<ViaticoPaisDto>> GetAll();
        Task<ViaticoPaisDto> GetById(int id);
    }
}
