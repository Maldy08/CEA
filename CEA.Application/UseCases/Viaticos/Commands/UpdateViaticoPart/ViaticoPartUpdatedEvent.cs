
using CEA.Domain.Common;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.Features.Viaticos.Commands.UpdateViaticoPart
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
