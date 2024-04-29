using CEA.Application.Common.Mappings;

namespace CEA.Application.DTOs.Transparencia
{
    public class ReporteDto : IMapFrom<ReporteDto>
    {
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
    }
}
