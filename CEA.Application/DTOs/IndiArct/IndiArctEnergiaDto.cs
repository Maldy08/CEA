using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.IndiArct;

namespace CEA.Application.DTOs.IndiArct
{
    public class IndiArctEnergiaDto : IMapFrom<IndiArctEnergia>
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public decimal Volumenes { get; set; }
        public decimal Kwh { get; set; }
        public decimal Costo { get; set; }
    }
}
