using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.CreateOficio
{
    public record CreateOficioCommandSP : IRequest<Result<OficioSpInsertarResult>>
    {
        public OficioDto OficioDto { get; set; } = new OficioDto();

    }

    internal class CreateOficioCommandSPHandler : IRequestHandler<CreateOficioCommandSP, Result<OficioSpInsertarResult>>
    {

        private readonly IOficioFunctions _oficioFunctions;

        public CreateOficioCommandSPHandler(IOficioFunctions oficioFunctions)
        {
            _oficioFunctions = oficioFunctions;
        }

        public async Task<Result<OficioSpInsertarResult>> Handle(CreateOficioCommandSP request, CancellationToken cancellationToken)
        {
            var result = await _oficioFunctions.SaveOficioSP(request.OficioDto);
            return Result<OficioSpInsertarResult>.Success(result);
        }
    }
}
