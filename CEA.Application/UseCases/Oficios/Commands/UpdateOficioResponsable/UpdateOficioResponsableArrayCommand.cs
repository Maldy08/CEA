using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.UseCases.Oficios.Commands.DeleteOficioResponsable;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.UpdateOficioResponsable
{

    public record UpdateOficioResponsableArrayCommand : IRequest<Result<int>>
    {

        public List<OficioResponsableDto> oficioResponsableDtos { get; set; } = new List<OficioResponsableDto>();

    }

    internal class UpdateOficioResponsableArrayCommandHandler : IRequestHandler<UpdateOficioResponsableArrayCommand, Result<int>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IOficioResponsableRepository _oficioResponsableRepository2;
        private readonly IMediator _mediator;

        public UpdateOficioResponsableArrayCommandHandler(IUnitOfWork unitOfWork, IOficioResponsableRepository oficioResponsableRepository2, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _oficioResponsableRepository2 = oficioResponsableRepository2;
            _mediator = mediator;
        }


        public async Task<Result<int>> Handle(UpdateOficioResponsableArrayCommand request, CancellationToken cancellationToken)
        {
            var ejercicio = request.oficioResponsableDtos[0].Ejercicio;
            var folio = request.oficioResponsableDtos[0].Folio;
            var eor = request.oficioResponsableDtos[0].Eor;
            var entidadesOriginales = await _oficioResponsableRepository2.GetOficioReponsableByEjercicioFolioEor(ejercicio, folio, eor);
            var count = entidadesOriginales.Count;

            for (int i = 0; i < count; i++)
            {
                var commandDelete = new DeleteOficioResponsableCommand(entidadesOriginales[i].Ejercicio, entidadesOriginales[i].Folio, entidadesOriginales[i].Eor, entidadesOriginales[i].IdEmpleado, entidadesOriginales[i].Rol, entidadesOriginales[i].Id);
                await _mediator.Send(commandDelete);
            }


            for (int i = 0; i < request.oficioResponsableDtos.Count; i++)
            {
                var entity = new OficioResponsable
                {
                    Ejercicio = request.oficioResponsableDtos[i].Ejercicio,
                    Folio = request.oficioResponsableDtos[i].Folio,
                    Eor = request.oficioResponsableDtos[i].Eor,
                    IdEmpleado = request.oficioResponsableDtos[i].IdEmpleado,
                    Rol = request.oficioResponsableDtos[i].Rol,
                    FAsignado = DateTime.Now,
                };
                await _unitOfWork.Repository<OficioResponsable>().AddAsync(entity);
            }

            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(Result<int>.Success(1));
        }
    }

}
