using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Oficios;

namespace CEA.Application.DTOs.Oficios
{
    public class OficioGpiDto : IMapFrom<OficioGpi>
    {
        public int IdGpi { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string PrimerRen { get; set; } = string.Empty;
        public string SegundoRen { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FechaCaptura { get; set; }
        public int Activo { get; set; }
    }
}
