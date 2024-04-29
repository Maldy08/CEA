

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Application.DTOs.Transparencia
{
    public class GetNombreFormatoDto : IMapFrom<Reporte>
    {
        public string Nombre { get; set; } = string.Empty;
    }
}
