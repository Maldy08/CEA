

using CEA.Domain.Common;

namespace CEA.Domain.Entities.Oficios
{
    public class OficioBitacora : BaseAuditableEntity
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public DateTime FechaCaptura { get; set; }
        public int IdEmpleado { get; set; }
        public int Estatus { get; set; }
        public string Comentarios { get; set; } = string.Empty;
    }
}
