using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.UseCases.Oficios.Commands.UpdateOficioResponsable
{
    public record UpdateOficioResponsableCommand : IRequest<Result<int>>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int IdEmpleado { get; set; }
        public int Rol { get; set; }
    }
    internal class UpdateOficioResponsableCommandHandler : IRequestHandler<UpdateOficioResponsableCommand, Result<int>>

    {
        private readonly IOficioResponsableRepository _oficioResponsableRepository;
        private readonly IOficioRepository _oficioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOficioResponsableCommandHandler(IOficioResponsableRepository oficioResponsableRepository, IUnitOfWork unitOfWork, IOficioRepository oficioRepository)
        {
            _oficioResponsableRepository = oficioResponsableRepository;
            _unitOfWork = unitOfWork;
            _oficioRepository = oficioRepository;
        }

        public async Task<Result<int>> Handle(UpdateOficioResponsableCommand request, CancellationToken cancellationToken)
        {
            var entities = await _oficioResponsableRepository.GetOficioReponsableByEjercicioFolioEor(request.Ejercicio, request.Folio, request.Eor);
            var oficio = await _oficioRepository.GetOficioByFolio(request.Ejercicio, request.Folio, request.Eor);

            


            return Result<int>.Success(1);
        }
    }
}
