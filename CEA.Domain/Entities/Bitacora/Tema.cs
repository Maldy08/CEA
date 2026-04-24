using CEA.Domain.Common;

namespace CEA.Domain.Entities.Bitacora
{
    public class Tema : BaseAuditableEntity
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaLimite { get; set; }
        public int IdDepartamentoOrigen { get; set; }
    }
}
