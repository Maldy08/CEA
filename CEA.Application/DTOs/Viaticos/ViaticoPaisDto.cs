

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.DTOs.Viaticos
{
    public class ViaticoPaisDto: IMapFrom<ViaticoPais>
    {
        public int IdPais { get; set; }
        public string Pais { get; set; } = string.Empty;

    }
}
