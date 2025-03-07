using CEA.Application.Common.Mappings;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.CreateOficioBitacora
{

    public record CreateOficioBitacoraSPCommand : IRequest<Result<int>>, IMapFrom<OficioBitacoraDto>
    {
        public List<OficioBitacoraDto> oficioBitacoraDtos { get; set; } = new List<OficioBitacoraDto>();
    }
    internal class CreateOficioBitacoraSPCommandHandler : IRequestHandler<CreateOficioBitacoraSPCommand, Result<int>>
    {
        private readonly IOficioFunctions _oficioFunctions;

        public CreateOficioBitacoraSPCommandHandler(IOficioFunctions oficioFunctions)
        {
            _oficioFunctions = oficioFunctions;
        }

        public async Task<Result<int>> Handle(CreateOficioBitacoraSPCommand request, CancellationToken cancellationToken)
        {


            await _oficioFunctions.SaveBitacoraOficio(request.oficioBitacoraDtos);
            return Result<int>.Success(1);
        }
    }
}
