
using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.DTOs
{
    public class ViaticoPartDto : IMapFrom<ViaticoPart>
    {
        public int Oficina { get; set; }
        public int Ejercicio { get; set; }
        public int NoViat { get; set; }
        public int Partida { get; set; }
        public double Importe { get; set; }

    }
}