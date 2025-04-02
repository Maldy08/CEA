using CEA.Application.DTOs.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.FoliarOficioSp
{
    public record FoliarOficioSpCommand : IRequest<Result<OficioSpActualizarFolioDto>>
    {
        public OficioSpActualizarFolioDto OficioSpActualizarFolioDto { get; set; } = new OficioSpActualizarFolioDto();

    }

    internal class FoliarOficioSpCommandHandler : IRequestHandler<FoliarOficioSpCommand, Result<OficioSpActualizarFolioDto>>
    {
        public Task<Result<OficioSpActualizarFolioDto>> Handle(FoliarOficioSpCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
