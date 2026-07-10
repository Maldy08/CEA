using CEA.Domain.Common;

namespace CEA.Domain.Entities.IndiArct
{
    public class IndiArctCatpresas : BaseAuditableEntity
    {
        // Id (heredado) -> ID_PRESA (asignado manualmente, catálogo)
        public string NombreOficial { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string? CorrientePrincipal { get; set; }
        public decimal? CapacidadNamoHm3 { get; set; }
        public string? UsoPrincipal { get; set; }
    }
}
