
using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Vehiculos;

namespace CEA.Application.DTOs.Vehiculos
{
    public class VsWtVehiculosDto :  IMapFrom<VhCatVehiculos>
    {
        public int Numero { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Placas { get; set; } = string.Empty;
        public string Serie { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Depto { get; set; } = string.Empty;


    }
}
