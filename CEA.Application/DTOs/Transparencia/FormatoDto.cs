
using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Application.DTOs.Transparencia
{
    public class FormatoDto : IMapFrom<Reporte>
    {

        public int IdArticulo { get; set; }

        public string IdAnexo { get; set; } = null!;

        public string IdAnexoInciso { get; set; } = null!;

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public int? Periocidad { get; set; }

        public string? Codigo { get; set; }
    }
}
