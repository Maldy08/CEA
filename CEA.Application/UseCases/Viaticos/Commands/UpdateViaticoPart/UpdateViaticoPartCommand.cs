

using AutoMapper;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Domain.Entities.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Commands.UpdateViaticoPart
{
    public record class UpdateViaticoPartCommand : IRequest<Result<int>>
    {
       // public int Id { get; set; }
        public int NoViat { get; set; }
        public int Ejercicio { get; set; }
        public int Partida { get; set; }
        public int Oficina { get; set; }
        public double Importe { get; set; }
    }

    internal class UpdateViaticoPartCommandHandler : IRequestHandler<UpdateViaticoPartCommand, Result<int>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IViaticoPartRepository _viaticoPartRepository;

        public UpdateViaticoPartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IViaticoPartRepository viaticoPartRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _viaticoPartRepository = viaticoPartRepository;
        }

        public async Task<Result<int>> Handle(UpdateViaticoPartCommand request, CancellationToken cancellationToken)
        {
            var viatico = await _viaticoPartRepository.GetByOficinaEjercicioNoviat(request.Oficina, request.Ejercicio, request.NoViat);

            if (viatico == null)
            {
                return await Result<int>.FailureAsync($"Viatico no encontrado.");
            }
            else
            {
                viatico.Importe = request.Importe;
                await _unitOfWork.Repository<ViaticoPart>().UpdateAsync(viatico);
                viatico.AddDomainEvent(new ViaticoPartUpdatedEvent(viatico));
                await _unitOfWork.Save(cancellationToken);
                return await Result<int>.SuccessAsync(viatico.Id);
            }
        }
    }
}
