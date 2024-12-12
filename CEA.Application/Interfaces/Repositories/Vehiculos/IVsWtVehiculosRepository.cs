

using CEA.Application.DTOs.Vehiculos;

namespace CEA.Application.Interfaces.Repositories.Vehiculos
{
    public interface IVsWtVehiculosRepository
    {
        Task<List<VsWtVehiculosDto>> GetVehiculos();
        Task<VsWtVehiculosDto> GetVehiculoByNoEconomico(int noEconomico);
    }
}
