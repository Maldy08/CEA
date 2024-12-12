

using CEA.Domain.Common;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.Features.Viaticos.Commands.CreateViaticoPart
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
