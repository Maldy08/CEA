

namespace CEA.Domain.Entities.Transparencia
{
    public class Departamento
    {
        public int IdDepto { get; set; }

        public string? Descripcion { get; set; }

        public string? CodigoDepto { get; set; }

        public virtual ICollection<AccesoReporte> AccesoReportes { get; set; } = new List<AccesoReporte>();

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
