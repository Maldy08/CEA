using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.UseCases.Oficios.Queries.GetOficiosMcByEorAndEjercicioAndFolio;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.UseCases.Oficios.Commands.DeleteOficioResponsable
{
    public record DeleteOficioResponsableCommand : IRequest<Result<int>>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int IdEmpleado { get; set; }
        public int Rol { get; set; }
    }

    internal class DeleteOficioResponsableCommandHandler : IRequestHandler<DeleteOficioResponsableCommand, Result<int>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IOficioResponsableRepository _oficioResponsableRepository;
        private readonly IMediator _mediator;


        public DeleteOficioResponsableCommandHandler(IUnitOfWork unitOfWork, IOficioResponsableRepository oficioResponsableRepository, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _oficioResponsableRepository = oficioResponsableRepository;
            _mediator = mediator;
        }

        public async Task<Result<int>> Handle(DeleteOficioResponsableCommand request, CancellationToken cancellationToken)
        {
            //var entityDto = await _oficioResponsableRepository.GetOficioResponsableByEjercicioFolioEorIdEmpleadoRol(request.Ejercicio, request.Folio, request.Eor, request.IdEmpleado, request.Rol);
            var entityDto = await _mediator.Send(new GetOficioResponsableByEjercicioFolioEorIdEmpleadoRolQuery(request.Ejercicio, request.Folio, request.Eor, request.IdEmpleado, request.Rol));
            
            if (entityDto == null)
            {
                return Result<int>.Failure("No se encontro el registro");
            }


            var entity = new OficioResponsable()
            {
                Ejercicio = entityDto.Data.Ejercicio,
                Folio = entityDto.Data.Ejercicio,
                Eor = entityDto.Data.Ejercicio,
                IdEmpleado = entityDto.Data.Ejercicio,
                Rol = entityDto.Data.Ejercicio,
                Id = entityDto.Data.Ejercicio,

            };


            await _unitOfWork.Repository<OficioResponsable>().DeleteAsync(entity);
            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(Result<int>.Success("Registro Eliminado"));

        }
    }
}
