using CEA.Domain.Common;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Domain.Events.Transparencia
{
    public class BitacoraDeletedEvent : BaseEvent
    {
        public BitacoraArchivo Bitacora { get; }

        public BitacoraDeletedEvent(BitacoraArchivo bitacora)
        {
            Bitacora = bitacora;
        }
    }
}
