

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Application.DTOs.Transparencia
{
    public class AccesoReporteDto : IMapFrom<AccesoReporte>
    {
        public int IdDepto { get; set; }
        public int IdArticulo { get; set; }
        public string IdAnexo { get; set; } = string.Empty;
        public string IdAnexoInciso { get; set; } = string.Empty;

    }
}
