using AutoMapper;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Domain.Entities.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Commands.UpdateViatico
{

    public record UpdateViaticoCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int Ejercicio { get; set; }

        public int Oficina { get; set; }
        public int NoViat { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }
        public int Dias { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaSal { get; set; }
        public DateTime FechaReg { get; set; }
        public string InforAct { get; set; } = string.Empty;
        public string InforResul { get; set; } = string.Empty;
        public DateTime InforFecha { get; set; }
    }

    internal class UpdateViaticoCommandHandler : IRequestHandler<UpdateViaticoCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IViaticoRepository _viaticoRepository;

        public UpdateViaticoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IViaticoRepository viaticoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _viaticoRepository = viaticoRepository;
        }

        public async Task<Result<int>> Handle(UpdateViaticoCommand request, CancellationToken cancellationToken)
        {
            //var viatico = await _unitOfWork.Repository<Viatico>().GetByIdAsync(request.Id);
            
            var viatico = await _viaticoRepository.GetAllByEjercicioOficinaNoviat(request.Fecha.Year, request.Oficina, request.NoViat);
;            if (viatico == null)
            {
                return await Result<int>.FailureAsync($"Viatico no encontrado.");
            }
            else
            {
                viatico.Motivo = request.Motivo;
                viatico.OrigenId = request.OrigenId;
                viatico.DestinoId = request.DestinoId;
                viatico.Dias = request.Dias;
                viatico.Fecha = request.Fecha;
                viatico.FechaSal = request.FechaSal;
                viatico.FechaReg = request.FechaReg;
                viatico.InforAct = request.InforAct;
                viatico.InforResul = request.InforResul;
                viatico.InforFecha = request.InforFecha;
                await _unitOfWork.Repository<Viatico>().UpdateAsync(viatico);
                viatico.AddDomainEvent(new ViaticoUpdatedEvent(viatico));
                await _unitOfWork.Save(cancellationToken);
                return await Result<int>.SuccessAsync(viatico.Id);
            }
        }
    
    
    }
}
