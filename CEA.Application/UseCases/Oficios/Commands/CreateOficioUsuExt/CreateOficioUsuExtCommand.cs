using CEA.Application.Common.Mappings;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.UseCases.Oficios.Commands.CreateOficioUsuExt
{
    public record CreateOficioUsuExtCommand : IRequest<Result<int>> , IMapFrom<OficioUsuExtDto>
    {
        public string Empresa { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Cargo { get; set; }
        public DateTime FechaCaptura { get; set; }
        public int Activo { get; set; }

    }
    internal class CreateOficioUsuExtCommandHandler: IRequestHandler<CreateOficioUsuExtCommand, Result<int>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public CreateOficioUsuExtCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateOficioUsuExtCommand request, CancellationToken cancellationToken)
        {
            var entity = new OficioUsuExt()
            {
                Empresa = request.Empresa,
                Siglas = request.Siglas,
                Nombre = request.Nombre,
                Cargo = request.Cargo,
                FechaCaptura = request.FechaCaptura,
                Activo = request.Activo
            };

            await _unitOfWork.Repository<OficioUsuExt>().AddAsync(entity);
            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(Result<int>.Success(1));

            
        }
    }
}
