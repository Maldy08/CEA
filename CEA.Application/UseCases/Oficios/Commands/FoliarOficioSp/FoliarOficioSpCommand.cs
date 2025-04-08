using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.FoliarOficioSp
{
    public record FoliarOficioSpCommand : IRequest<Result<OficioSpFoliarResult>>
    {
        public OficioSpActualizarFolioDto OficioSpActualizarFolioDto { get; set; } = new OficioSpActualizarFolioDto();

    }

    internal class FoliarOficioSpCommandHandler : IRequestHandler<FoliarOficioSpCommand, Result<OficioSpFoliarResult>>
    {

       private readonly IOficioFunctions _oficioFunctions;

        public FoliarOficioSpCommandHandler(IOficioFunctions oficioFunctions)
        {
            _oficioFunctions = oficioFunctions;
        }

        public async Task<Result<OficioSpFoliarResult>> Handle(FoliarOficioSpCommand request, CancellationToken cancellationToken)
        {
            var result = await _oficioFunctions.OficioSPFoliar(request.OficioSpActualizarFolioDto.Ejercicio, 
                request.OficioSpActualizarFolioDto.Folio, 
                    request.OficioSpActualizarFolioDto.Eor, 
                        request.OficioSpActualizarFolioDto.Empleado);

            return Result<OficioSpFoliarResult>.Success(result);
        }
    }
}
