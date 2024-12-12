using CEA.Domain.Common;
using CEA.Domain.Entities.Oficios;


namespace CEA.Application.Features.Oficios.Commands.CreateOficio
{
    public class OficioCreatedEvent : BaseEvent
    {
        public Oficio Oficio { get; set; }

        public OficioCreatedEvent(Oficio oficio)
        {
            Oficio = oficio;
        }
    }
}
