using CEA.Application.DTOs.Viaticos;

namespace CEA.Application.Interfaces.Repositories.Viaticos
{
    public interface IViaticoCiudadRepository
    {
        Task<List<ViaticoCiudadDto>> GetAll();
        Task<ViaticoCiudadDto> GetById(int id);

        Task<List<ViaticoCiudadDto>> GetByIdEstado(int id);
    }
}
