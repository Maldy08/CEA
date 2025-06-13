using CEA.Application.Common.Mappings;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.UseCases.Oficios.Commands.CreateOficioResponsable
{

    public record CreateOficioResponsableCommand : IRequest<Result<int>> , IMapFrom<OficioResponsableDto>
    {

        public List<OficioResponsableDto> oficioResponsables { get; set; } = new List<OficioResponsableDto>();

        //public int Ejercicio { get; set; }
        //public int Folio { get; set; }
        //public int Eor { get; set; }
        //public int IdEmpleado { get; set; }
        //public int Rol { get; set; }


    }
    internal class CreateOficioResponsableCommandHandler : IRequestHandler<CreateOficioResponsableCommand, Result<int>>
    {

        private readonly IUnitOfWork _unitOfWork;
        public CreateOficioResponsableCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateOficioResponsableCommand request, CancellationToken cancellationToken)
        {

            var x = "xxxx";

            foreach (var oficioResponsable in request.oficioResponsables)
            {
                var entity = new OficioResponsable
                {
                    Ejercicio = oficioResponsable.Ejercicio,
                    Folio = oficioResponsable.Folio,
                    Eor = oficioResponsable.Eor,
                    IdEmpleado = oficioResponsable.IdEmpleado,
                    Rol = oficioResponsable.Rol,
                    FAsignado = DateTime.Now,
                    //Iox = 1
                };
                await _unitOfWork.Repository<OficioResponsable>().AddAsync(entity);
            }

           // await _unitOfWork.Repository<OficioResponsable>().AddAsync(entity);
            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(Result<int>.Success(1));
        }
    }
}
