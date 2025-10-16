using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Oficios;

namespace CEA.Application.DTOs.Oficios
{
    public class OficioClasificacionDto : IMapFrom<OficioClasificacion>
    {
        public int Id { get; set; }
        public String Codigo { get; set; } = String.Empty;
        public String Nombre { get; set; } = String.Empty;
        public String Descripcion { get; set; } = String.Empty;
    }
}
