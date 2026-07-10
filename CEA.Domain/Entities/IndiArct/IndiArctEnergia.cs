using CEA.Domain.Common;

namespace CEA.Domain.Entities.IndiArct
{
    public class IndiArctEnergia : BaseAuditableEntity
    {
        // Id (heredado) -> ID_ARCT_ENERGIA (generado por secuencia/trigger Oracle)
        public int Anio { get; set; }
        public int Mes { get; set; }
        public decimal Volumenes { get; set; }
        public decimal Kwh { get; set; }
        public decimal Costo { get; set; }
    }
}
