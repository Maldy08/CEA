using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Nomina;

namespace CEA.Application.DTOs.Nomina
{
    public class NomPeriodosDto : IMapFrom<NomPeriodos>
    {
        public int TipoNom { get; set; }
        public int PerNom { get; set; }
        public DateTime FincDes { get; set; }
        public DateTime FinciHas { get; set; }
        public int AnoProceso { get; set; }
    }
}
