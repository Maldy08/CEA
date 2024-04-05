

using CEA.Domain.Common;

namespace CEA.Domain.Entities.Viaticos
{
    public class ViaticoOfi : BaseAuditableEntity
    {
        public int IdOfi { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string RutaTrans { get; set; } = string.Empty;

    }
}
