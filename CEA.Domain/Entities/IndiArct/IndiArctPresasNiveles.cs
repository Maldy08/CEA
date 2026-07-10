using CEA.Domain.Common;

namespace CEA.Domain.Entities.IndiArct
{
    public class IndiArctPresasNiveles : BaseAuditableEntity
    {
        // Id (heredado) -> ID_CAPTURA_VOLPRESAS (generado por secuencia/trigger Oracle)
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int IdPresa { get; set; }
        public decimal VolumenM3 { get; set; }
    }
}
