using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
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
        private readonly IGenericRepository<OficioResponsable> _oficioResponsableRepository;


        public UpdateOficioResponsableArrayCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<OficioResponsable> oficioResponsableRepository)
        {
            _unitOfWork = unitOfWork;
            _oficioResponsableRepository = oficioResponsableRepository;
        }


        public async Task<Result<int>> Handle(UpdateOficioResponsableArrayCommand request, CancellationToken cancellationToken)
        {
           

            var entityOficio = new List<OficioResponsable>();
            foreach (var oficioResponsableDto in request.oficioResponsableDtos)
            {
                var oficioResponsable = new OficioResponsable
                {
                    Ejercicio = oficioResponsableDto.Ejercicio,
                    Folio = oficioResponsableDto.Folio,
                    Eor = oficioResponsableDto.Eor,
                    IdEmpleado = oficioResponsableDto.IdEmpleado,
                    Rol = oficioResponsableDto.Rol
                };

                entityOficio.Add(oficioResponsable);

            }
            // Implementation here
            return await Task.FromResult(Result<int>.Success(1));
        }
    }

}
