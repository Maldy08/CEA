

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Oficios;

namespace CEA.Application.DTOs.Oficios
{
    public class OficioParametroDto : IMapFrom<OficioParametro>
    {
        public int Ejercicio { get; set; }
        public int NextFRec { get; set; }
        public int NextFEnv { get; set; }
        public int NextFXexp { get; set; }
    }
}
