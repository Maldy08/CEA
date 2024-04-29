

using CEA.Domain.Common;

namespace CEA.Domain.Entities.Transparencia
{
    public class Puesto : BaseAuditableEntity
    {
        public int IdPuesto { get; set; }

        public string? Descripcion { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
