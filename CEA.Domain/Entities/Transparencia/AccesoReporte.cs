
namespace CEA.Domain.Entities.Transparencia
{
    public class AccesoReporte
    {
        public int Id { get; set; }

        public int IdDepto { get; set; }

        public int IdArticulo { get; set; }

        public string IdAnexo { get; set; } = null!;

        public string IdAnexoInciso { get; set; } = null!;

        public int? Estatus { get; set; }

        public virtual Departamento IdDeptoNavigation { get; set; } = null!;

        public virtual Reporte Reporte { get; set; } = null!;
    }
}
