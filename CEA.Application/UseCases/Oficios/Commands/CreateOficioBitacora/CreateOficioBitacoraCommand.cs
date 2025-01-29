using CEA.Application.Common.Mappings;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.UseCases.Oficios.Commands.CreateOficioBitacora
{

    public record  CreateOficioBitacoraCommand : IRequest<Result<int>> , IMapFrom<OficioBitacoraDto>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public DateTime FechaCaptura { get; set; }
        public int IdEmpleado { get; set; }
        public int Estatus { get; set; }
        public string Comentarios { get; set; } = string.Empty;

    }
    internal class CreateOficioBitacoraCommandHandler: IRequestHandler<CreateOficioBitacoraCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOficioRepository _oficioRepository;

        public CreateOficioBitacoraCommandHandler(IUnitOfWork unitOfWork, IOficioRepository oficioRepository)
        {
            _unitOfWork = unitOfWork;
            _oficioRepository = oficioRepository;
        }

        public async Task<Result<int>> Handle(CreateOficioBitacoraCommand request, CancellationToken cancellationToken)
        {
            //var oficio = await _oficioRepository.GetOficioByFolio(request.Ejercicio, request.Folio, request.Eor);

            var entity = new OficioBitacora()
            {
                Ejercicio = request.Ejercicio,
                Folio = request.Folio,
                Eor = request.Eor,
                FechaCaptura = request.FechaCaptura,
                IdEmpleado = request.IdEmpleado,
                Estatus = request.Estatus,
                Comentarios = request.Comentarios
            };
            await _unitOfWork.Repository<OficioBitacora>().AddAsync(entity);
            try
                {
                await _unitOfWork.Save(cancellationToken);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Result<int>.Failure(ex.Message));
            }
            return await Task.FromResult(Result<int>.Success("Bitacora guardada exitosamente!"));
        }
    
    
    }
}
