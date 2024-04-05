
using CEA.Domain.Common;

namespace CEA.Domain.Entities.Viaticos
{
    public class ViaticoEstado : BaseAuditableEntity
    {
        public int IdEstado { get; set; }
        public int IdPais { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
