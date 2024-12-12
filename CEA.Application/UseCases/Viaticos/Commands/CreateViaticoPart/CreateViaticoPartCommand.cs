

using AutoMapper;
using CEA.Application.Common.Mappings;
using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Entities.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Commands.CreateViaticoPart
{
    public class CreateViaticoPartCommand : IRequest<Result<int>> , IMapFrom<ViaticoPart>
    {
        public int Oficina { get; set; }
        public int Ejercicio { get; set; }
        public int NoViat { get; set; }
        public int Partida { get; set; }
        public double Importe { get; set; }


    }

    internal class CreateViaticoPartCommandHandler : IRequestHandler<CreateViaticoPartCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateViaticoPartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateViaticoPartCommand request, CancellationToken cancellationToken)
        {
            var viaticoPart = new ViaticoPart()
            {
                Oficina = request.Oficina,
                Ejercicio = request.Ejercicio,
                NoViat = request.NoViat,
                Partida = request.Partida,
                Importe = request.Importe
            };

            await _unitOfWork.Repository<ViaticoPart>().AddAsync(viaticoPart);
            viaticoPart.AddDomainEvent(new ViaticoPartCreatedEvent(viaticoPart));
            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(viaticoPart.Id);
        }
    }
}
