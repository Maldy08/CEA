
using CEA.Domain.Common;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Application.Features.Transparencia.Commands.UpdateBitacora
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
