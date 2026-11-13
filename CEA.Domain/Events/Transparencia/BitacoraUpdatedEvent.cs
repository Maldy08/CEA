using CEA.Domain.Common;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Domain.Events.Transparencia
{
    public class BitacoraUpdatedEvent : BaseEvent
    {
        public BitacoraArchivo Bitacora { get; }

        public BitacoraUpdatedEvent(BitacoraArchivo bitacora)
        {
            Bitacora = bitacora;
        }
    }
}
