

using CEA.Domain.Common;

namespace CEA.Domain.Entities.Oficios
{
    public class OficioEstatus : BaseAuditableEntity
    {
        public int IdEstatus { get; set; }
        public int Eor { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
