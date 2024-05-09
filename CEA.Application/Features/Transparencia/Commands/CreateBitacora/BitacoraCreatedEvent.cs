

using CEA.Domain.Common;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Application.Features.Transparencia.Commands.CreateBitacora
{
    public class BitacoraCreatedEvent : BaseEvent
    {
        public BitacoraArchivo Bitacora { get; set; }

        public BitacoraCreatedEvent(BitacoraArchivo bitacora)
        {
            Bitacora = bitacora;
        }
    }
}
