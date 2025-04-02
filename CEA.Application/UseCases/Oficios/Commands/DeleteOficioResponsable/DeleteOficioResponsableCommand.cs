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
        public int Id { get; set; }

        public DeleteOficioResponsableCommand(int ejercicio, int folio, int eor, int idEmpleado, int rol, int id)
        {
            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
            IdEmpleado = idEmpleado;
            Rol = rol;
            Id = id;
        }

    }

    internal class DeleteOficioResponsableCommandHandler : IRequestHandler<DeleteOficioResponsableCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteOficioResponsableCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteOficioResponsableCommand request, CancellationToken cancellationToken)
        {

            var entity = new OficioResponsable()
            {
                Ejercicio = request.Ejercicio,
                Folio = request.Folio,
                Eor = request.Eor,
                IdEmpleado = request.IdEmpleado,
                Rol = request.Rol,
                Id = request.Id,

            };


            await _unitOfWork.Repository<OficioResponsable>().DeleteAsync(entity);
           // await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(Result<int>.Success("Registro Eliminado"));

        }
    }
}
