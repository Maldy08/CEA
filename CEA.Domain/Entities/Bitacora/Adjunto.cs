using CEA.Domain.Common;

namespace CEA.Domain.Entities.Bitacora
{
    public class Adjunto : BaseAuditableEntity
    {
        public int IdAvance { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? TipoMime { get; set; }
    }
}
