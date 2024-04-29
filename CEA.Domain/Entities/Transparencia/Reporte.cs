

using CEA.Domain.Common;

namespace CEA.Domain.Entities.Transparencia
{
    public class Reporte : BaseAuditableEntity
    {
        public int IdArticulo { get; set; }

        public string IdAnexo { get; set; } = null!;

        public string IdAnexoInciso { get; set; } = null!;

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public int? Periocidad { get; set; }

        public string? Codigo { get; set; }

        public virtual ICollection<AccesoReporte> AccesoReportes { get; set; } = new List<AccesoReporte>();
    }
}
