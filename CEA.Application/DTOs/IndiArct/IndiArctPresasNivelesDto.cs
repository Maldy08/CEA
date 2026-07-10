using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.IndiArct;

namespace CEA.Application.DTOs.IndiArct
{
    public class IndiArctPresasNivelesDto : IMapFrom<IndiArctPresasNiveles>
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int IdPresa { get; set; }
        public decimal VolumenM3 { get; set; }
        public string? NombrePresa { get; set; }
    }
}
