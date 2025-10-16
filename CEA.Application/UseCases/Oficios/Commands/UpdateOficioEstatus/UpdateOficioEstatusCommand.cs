using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.UpdateOficioEstatus
{

    public record class UpdateOficioEstatusCommand: IRequest<Result<int>>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int Estatus { get; set; }
    }


    internal class UpdateOficioEstatusCommandHandler : IRequestHandler<UpdateOficioEstatusCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;   
        private readonly IOficioRepository _oficioRepository;

        public UpdateOficioEstatusCommandHandler(IUnitOfWork unitOfWork, IOficioRepository oficioRepository)
        {
            _unitOfWork = unitOfWork;
            _oficioRepository = oficioRepository;
        }
        public async Task<Result<int>> Handle(UpdateOficioEstatusCommand request, CancellationToken cancellationToken)
        {
            var oficio = await _oficioRepository.GetOficio(request.Ejercicio, request.Folio, request.Eor);

            if (oficio == null)
            {
                return Result<int>.Failure("No se encontró el oficio.");
            }

            oficio.Estatus = request.Estatus;
            await _unitOfWork.Repository<Oficio>().UpdateAsync(oficio);
            await _unitOfWork.Save(cancellationToken);

            return Result<int>.Success(oficio.Folio, "Estatus del oficio actualizado correctamente.");


        }
    }
}
