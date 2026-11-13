using CEA.Domain.Common;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Domain.Events.Viaticos
{
    public class ViaticoUpdatedEvent : BaseEvent
    {
        public Viatico Viatico { get; }

        public ViaticoUpdatedEvent(Viatico viatico)
        {
            Viatico = viatico;
        }
    }
}
