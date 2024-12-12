

using CEA.Domain.Common;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.Features.Viaticos.Commands.UpdateViatico
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
