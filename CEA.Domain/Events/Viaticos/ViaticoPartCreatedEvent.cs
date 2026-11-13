using CEA.Domain.Common;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Domain.Events.Viaticos
{
    public class ViaticoPartCreatedEvent : BaseEvent
    {
        public ViaticoPart ViaticoPart { get; set; }

        public ViaticoPartCreatedEvent(ViaticoPart viaticoPart)
        {
            ViaticoPart = viaticoPart;
        }
    }
}
