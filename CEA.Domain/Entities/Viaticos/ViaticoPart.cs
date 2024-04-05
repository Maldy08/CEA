using CEA.Domain.Common;

namespace CEA.Domain.Entities.Viaticos
{
    public class ViaticoPart : BaseAuditableEntity
    {
        public int Oficina { get; set; }
        public int Ejercicio { get; set; }
        public int NoViat { get; set; }
        public int Partida { get; set; }
        public double Importe { get; set; }
    }
}
