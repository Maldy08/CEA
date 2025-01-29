using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Domain.Entities.Oficios;
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
            entities = entities.Where(x => x.Rol != 3).ToList();

            var oficio = await _oficioRepository.GetOficioByFolio(request.Ejercicio, request.Eor, request.Folio);

            if (entities.Count > 0)
            {
                var oficioResponsable = new OficioResponsable();
                var oficioResponsableUpdate = new OficioResponsable();
                var entityOficio = new List<OficioResponsable>();

                foreach (var entity in entities)
                {

                    if (entity.IdEmpleado == request.IdEmpleado )
                         return Result<int>.Success(1);
                    if (entity.Rol == 1)
                    {
                        oficioResponsable.Ejercicio = request.Ejercicio;
                        oficioResponsable.Folio = request.Folio;
                        oficioResponsable.Eor = request.Eor;
                        oficioResponsable.IdEmpleado = request.IdEmpleado;
                        oficioResponsable.Rol = 1;

                        oficioResponsableUpdate.Id = entity.Id;
                        oficioResponsableUpdate.Ejercicio = request.Ejercicio;
                        oficioResponsableUpdate.Folio = request.Folio;
                        oficioResponsableUpdate.Eor = request.Eor;
                        oficioResponsableUpdate.IdEmpleado = entity.IdEmpleado;
                        oficioResponsableUpdate.Rol = 2;

                        entityOficio.Add(oficioResponsable);
                        entityOficio.Add(oficioResponsableUpdate);

                        //await _unitOfWork.Repository<OficioResponsable>().UpdateAsync(oficioResposableUpdate);
                        //await _unitOfWork.Repository<OficioResponsable>().AddAsync(oficioResponsable);
                        //await _unitOfWork.Save(cancellationToken);

                    }

                    var eliminar = new OficioResponsable()
                    {
                        Id = entity.Id,
                        Ejercicio = entity.Ejercicio,
                        Folio = entity.Folio,
                        Eor = entity.Eor,
                        IdEmpleado = entity.IdEmpleado,
                        Rol = entity.Rol
                    };

                    await _unitOfWork.Repository<OficioResponsable>().DeleteAsync(eliminar);
                    await _unitOfWork.Save(cancellationToken);


                }

                foreach (var entity in entityOficio)
                {
                    await _unitOfWork.Repository<OficioResponsable>().AddAsync(entity);
                    await _unitOfWork.Save(cancellationToken);
                }


            }

            return Result<int>.Success(1);
        }
    }
}
