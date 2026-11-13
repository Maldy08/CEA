using CEA.Domain.Common;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Domain.Events.Viaticos
{
    public class ViaticoPartUpdatedEvent : BaseEvent
    {
        public ViaticoPart ViaticoPart { get; set; }

        public ViaticoPartUpdatedEvent(ViaticoPart viaticoPart)
        {
            ViaticoPart = viaticoPart;
        }
    }
}
