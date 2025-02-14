

using CEA.Domain.Common;

namespace CEA.Domain.Entities.Oficios
{
    public class OficioGpi : BaseAuditableEntity
    {
        public int IdGpi { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string PrimerRen { get; set; } = string.Empty;
        public string SegundoRen { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FechaCaptura { get; set; }
        public int Activo { get; set; }

    }
}
