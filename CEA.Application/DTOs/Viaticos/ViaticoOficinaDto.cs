

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.DTOs.Viaticos
{
    public class ViaticoOficinaDto : IMapFrom<ViaticoOfi>
    {
        public int IdOfi { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string RutaTrans { get; set; } = string.Empty;
    }
}
