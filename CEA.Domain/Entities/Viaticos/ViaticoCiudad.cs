

using CEA.Domain.Common;

namespace CEA.Domain.Entities.Viaticos
{
    public class ViaticoCiudad : BaseAuditableEntity
    {
        public int IdCiudad { get; set; }
        public int IdEstado { get; set; }
        public string Ciudad { get; set; } = string.Empty;

    }
}
