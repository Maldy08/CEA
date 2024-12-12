

using CEA.Domain.Common;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Application.Features.Transparencia.Commands.CreateBitacora
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
