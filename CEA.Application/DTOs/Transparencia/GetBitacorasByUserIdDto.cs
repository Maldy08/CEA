

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Application.DTOs.Transparencia
{
    public class GetBitacorasByUserIdDto : IMapFrom<BitacoraArchivo>
    {
        public int Id { get; set; }
        public string NombreReporte { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Hipervinculo { get; set; } = string.Empty;
    }
}
