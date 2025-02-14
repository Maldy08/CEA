using CEA.Domain.Common;

namespace CEA.Domain.Entities.Oficios
{
    public class OficioGpiEmp : BaseAuditableEntity
    {
        public int IdGpiEmp { get; set; }
        public int IdGpi { get; set; }
        public int NoEmp { get; set; }
    }
}
