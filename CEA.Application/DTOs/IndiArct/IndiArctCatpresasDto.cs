using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.IndiArct;

namespace CEA.Application.DTOs.IndiArct
{
    public class IndiArctCatpresasDto : IMapFrom<IndiArctCatpresas>
    {
        public int Id { get; set; }
        public string NombreOficial { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string? CorrientePrincipal { get; set; }
        public decimal? CapacidadNamoHm3 { get; set; }
        public string? UsoPrincipal { get; set; }
    }
}
