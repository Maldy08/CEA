using System.Collections.Generic;
using CEA.Domain.Common;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Domain.Events.Transparencia
{
    public class BitacoraCreatedEvent : BaseEvent
    {
        public List<BitacoraArchivo> Bitacora { get; set; }

        public BitacoraCreatedEvent(List<BitacoraArchivo> bitacora)
        {
            Bitacora = bitacora;
        }
    }
}
