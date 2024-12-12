

using CEA.Domain.Entities.Oficios;

namespace CEA.Application.UseCases.Oficios.Commands.CreateOficioResponsable
{
    public class OficioResponsableCraetedEvent
    {
        public OficioResponsable oficioResponsable { get; set; }

        public OficioResponsableCraetedEvent(OficioResponsable oficioResponsable)
        {
            this.oficioResponsable = oficioResponsable;
        }
    }
}
