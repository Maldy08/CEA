using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.UpdateOficioUsuExt
{
    public record UpdateOficioUsuExtCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int IdExterno { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Cargo { get; set; }
        public DateTime FechaCaptura { get; set; }
        public int Activo { get; set; }


    }
    internal class UpdateOficioUsuExtCommandHandler : IRequestHandler<UpdateOficioUsuExtCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOficioUsuExtCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(UpdateOficioUsuExtCommand request, CancellationToken cancellationToken)
        {
            var entidad = new OficioUsuExt
            {
                Id = request.Id,
                IdExterno = request.IdExterno,
                Empresa = request.Empresa,
                Siglas = request.Siglas,
                Nombre = request.Nombre,
                Cargo = request.Cargo,
                FechaCaptura = request.FechaCaptura,
                Activo = request.Activo
            };

            await _unitOfWork.Repository<OficioUsuExt>().UpdateAsync(entidad);
            try
            {
                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync("Actualizacion correcta");
            }
            catch (Exception ex)
            {
                return await Result<int>.FailureAsync(ex.Message);
            }
        }
    }
}
