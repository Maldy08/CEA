

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Oficios;
using System.ComponentModel.DataAnnotations.Schema;

namespace CEA.Application.DTOs.Oficios
{
    public class OficioBitacoraDto: IMapFrom<OficioBitacora>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public DateTime FechaCaptura { get; set; }
        public int IdEmpleado { get; set; }
        public int Estatus { get; set; }
        public string Comentarios { get; set; } = string.Empty;

        [NotMapped]
        public string Usuario { get; set; } = string.Empty;
    }
}
