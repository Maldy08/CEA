using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Oficios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Application.DTOs.Oficios
{
    public class OficioResponsableDto : IMapFrom<OficioResponsable>
    {
        public int Id { get; set; }
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int IdEmpleado { get; set; }
        public int Rol { get; set; }
    }
}
