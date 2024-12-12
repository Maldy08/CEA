
using CEA.Application.DTOs.Vehiculos;

namespace CEA.Application.Interfaces.Repositories.Vehiculos
{
    public interface IVsListaVehiculosRepository
    {
        Task<List<VsListaVehiculosDto>> GetAllVehiculos();

    }
}
