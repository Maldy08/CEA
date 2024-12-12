using CEA.Application.Common.Mappings;
using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Entities.Transparencia;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.Features.Transparencia.Commands.DeleteBitacora
{
    public record DeleteBitacoraCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public DeleteBitacoraCommand(int id)
        {
            Id = id;
        }

    }

    internal class DeleteBitacoraCommandHandler : IRequestHandler<DeleteBitacoraCommand, Result<int>>
    {
        private readonly IUnitOfWorkSQL _unitOfWork;

        public DeleteBitacoraCommandHandler(IUnitOfWorkSQL unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(DeleteBitacoraCommand request, CancellationToken cancellationToken)
        {
            var bitacora = await _unitOfWork.Repository<BitacoraArchivo>().GetByIdAsync(request.Id);

            if (bitacora == null)
            {
                return await Result<int>.FailureAsync($"No se encontró la bitácora con el id {request.Id}");
            }

            var archivo = new FileInfo(@bitacora.RutaArchivo!);
            if (archivo.Exists)
            {
                archivo.Delete();
            }

            await _unitOfWork.Repository<BitacoraArchivo>().DeleteAsync(bitacora);
            bitacora.AddDomainEvent(new BitacoraDeletedEvent(bitacora));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(bitacora.Id, "Bitacora Eliminada");

        }
    }

}
