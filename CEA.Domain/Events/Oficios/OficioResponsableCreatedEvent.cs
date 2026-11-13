using CEA.Domain.Common;
using CEA.Domain.Entities.Oficios;

namespace CEA.Domain.Events.Oficios
{
    public class OficioResponsableCreatedEvent : BaseEvent
    {
        public OficioResponsable OficioResponsable { get; set; }

        public OficioResponsableCreatedEvent(OficioResponsable oficioResponsable)
        {
            OficioResponsable = oficioResponsable;
        }
    }
}
