using CEA.Domain.Common;

namespace CEA.Domain.Entities.Oficios
{
    public class OficioResponsable : BaseAuditableEntity
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int IdEmpleado { get; set; }
        public int Rol { get; set; }
        public int Iox { get; set; }
    }
}
