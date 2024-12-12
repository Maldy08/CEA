using CEA.Domain.Common;
using CEA.Domain.Entities.Transparencia;


namespace CEA.Application.Features.Transparencia.Commands.DeleteBitacora
{
    public class BitacoraDeletedEvent :  BaseEvent
    {

        public BitacoraArchivo Bitacora { get;}

        public BitacoraDeletedEvent(BitacoraArchivo bitacora)
        {
            Bitacora = bitacora;
        }

    }
}
