using CEA.Domain.Common;
using CEA.Domain.Entities.Viaticos;


namespace CEA.Application.Features.Viaticos.Commands.CreateViatico
{
    public class ViaticoCreatedEvent : BaseEvent
    {
        public Viatico Viatico { get; set; }

        public ViaticoCreatedEvent(Viatico viatico)
        {
            Viatico = viatico;
        }
    }
}
