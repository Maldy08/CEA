
using CEA.Domain.Common;

namespace CEA.Domain.Entities.Viaticos
{
    public class ViaticoPais : BaseAuditableEntity
    {
        public int IdPais { get; set; }
        public string Pais { get; set; } = string.Empty; 
    }
}
