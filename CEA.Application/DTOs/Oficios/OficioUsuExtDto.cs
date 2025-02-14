

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Oficios;

namespace CEA.Application.DTOs.Oficios
{
    public class OficioUsuExtDto : IMapFrom<OficioUsuExt>
    {
        public int IdExterno { get; set; }
        public int Frecuencia { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Cargo { get; set; }
        
    }
}
