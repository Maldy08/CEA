
using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.DTOs.Viaticos
{
    public class ViaticoCiudadDto: IMapFrom<ViaticoCiudad>
    {
        public int IdCiudad { get; set; }
        public int IdEstado { get; set; }
        public string Ciudad { get; set; } = string.Empty;
    }
}
