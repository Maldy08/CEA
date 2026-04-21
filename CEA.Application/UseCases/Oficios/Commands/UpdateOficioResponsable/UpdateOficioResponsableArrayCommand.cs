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

            if (count == 0)
            {
                return await Task.FromResult(Result<int>.Failure("No existen registros para actualizar"));
            }


            var temporales = new List<OficioResponsable>();

            foreach (var dto in request.oficioResponsableDtos)
            {
                var existente = entidadesOriginales
                    .FirstOrDefault(e => e.IdEmpleado == dto.IdEmpleado && e.Rol == dto.Rol);

                if (existente == null)
                {
                    temporales.Add(new OficioResponsable
                    {
                        Ejercicio = dto.Ejercicio,
                        Folio = dto.Folio,
                        Eor = dto.Eor,
                        IdEmpleado = dto.IdEmpleado,
                        Rol = dto.Rol,
                        FAsignado = DateTime.Now,
                    });
                }
                else
                {
                    var oficioResponsable = await _oficioResponsableRepository2.GetOficioReponsableByEjercicioFolioEorNoDto(dto.Ejercicio, dto.Folio, dto.Eor, existente.IdEmpleado, existente.Rol);

                    temporales.Add(new OficioResponsable
                    {
                        Ejercicio = dto.Ejercicio,
                        Folio = dto.Folio,
                        Eor = dto.Eor,
                        IdEmpleado = existente.IdEmpleado,
                        Rol = existente.Rol,
                        FAsignado = oficioResponsable?.FAsignado,
                    });
                }
            }

            for (int i = 0; i < count; i++)
            {
                var commandDelete = new DeleteOficioResponsableCommand(entidadesOriginales[i].Ejercicio, entidadesOriginales[i].Folio, entidadesOriginales[i].Eor, entidadesOriginales[i].IdEmpleado, entidadesOriginales[i].Rol, entidadesOriginales[i].Id);
                await _mediator.Send(commandDelete);
            }


            for (int i = 0; i < temporales.Count; i++)
            {
                var entity = new OficioResponsable
                {
                    Ejercicio = temporales[i].Ejercicio,
                    Folio = temporales[i].Folio,
                    Eor = temporales[i].Eor,
                    IdEmpleado = temporales[i].IdEmpleado,
                    Rol = temporales[i].Rol,
                    FAsignado = temporales[i].FAsignado,
                    Iox = 1,
                };
                await _unitOfWork.Repository<OficioResponsable>().AddAsync(entity);
            }

            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(Result<int>.Success("Datos actualizados correctamente!"));
        }
    }

}
