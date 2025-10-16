

using CEA.Domain.Common;

namespace CEA.Domain.Entities.Oficios
{
    public class OficioUsuExt : BaseAuditableEntity
    {

        public int IdExterno { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Cargo { get; set; }
        public DateTime FechaCaptura { get; set; }
        public int Activo { get; set; }
    }
}
