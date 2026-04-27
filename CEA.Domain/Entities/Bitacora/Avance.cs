using CEA.Domain.Common;

namespace CEA.Domain.Entities.Bitacora
{
    public class Avance : BaseAuditableEntity
    {
        public int IdTema { get; set; }
        public int IdUsuario { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public DateTime? FechaEdicion { get; set; }
    }
}
