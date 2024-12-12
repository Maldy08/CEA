using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Oficios;

namespace CEA.Application.DTOs.Oficios
{
    public class OficioEstatusDto : IMapFrom<OficioEstatus>
    {
        public int IdEstatus { get; set; }
        public int Eor { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
