

using CEA.Domain.Common;

namespace CEA.Domain.Entities.Transparencia
{
    public class Usuario : BaseAuditableEntity
    {
        public int IdUsuario { get; set; }

        public string Usuario1 { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int IdNivel { get; set; }

        public int Activo { get; set; }

        public int IdDepto { get; set; }

        public int IdPuesto { get; set; }

        public virtual Departamento IdDeptoNavigation { get; set; } = null!;

        public virtual UsuariosNivele IdNivelNavigation { get; set; } = null!;

        public virtual Puesto IdPuestoNavigation { get; set; } = null!;
    }
}
