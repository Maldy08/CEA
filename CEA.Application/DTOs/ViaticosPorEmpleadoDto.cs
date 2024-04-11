using CEA.Application.Common.Mappings;
using CEA.Domain.Common;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.DTOs
{
    public class ViaticosPorEmpleadoDto : IMapFrom<ViaticosPorEmpleadoDto>
    {
        public int Viatico { get; set; }
        public DateTime Fecha { get; set; }
        public string Origen { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public DateTime Salida { get; set; }
        public DateTime Regreso { get; set; }
        public string Estatus { get; set; } = string.Empty;
        public int Oficina { get; set; }    
        public int Ejercicio { get; set; }  
    }
}
