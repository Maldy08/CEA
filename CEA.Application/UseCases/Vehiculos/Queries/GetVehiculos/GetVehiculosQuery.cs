using AutoMapper;
using CEA.Application.DTOs.Vehiculos;
using CEA.Application.Interfaces.Repositories.Vehiculos;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.Features.Vehiculos.Queries.GetVehiculos
{
    public record GetVehiculosQuery : IRequest<Result<List<VsWtVehiculosDto>>>;


    internal class GetVehiculosQueryHandler : IRequestHandler<GetVehiculosQuery, Result<List<VsWtVehiculosDto>>>
    {
        public readonly IVsWtVehiculosRepository _vehiculosRepository;
        public readonly IMapper _mapper;

        public GetVehiculosQueryHandler(IVsWtVehiculosRepository vehiculosRepository, IMapper mapper)
        {
            _vehiculosRepository = vehiculosRepository ?? throw new ArgumentNullException(nameof(vehiculosRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<VsWtVehiculosDto>>> Handle(GetVehiculosQuery request, CancellationToken cancellationToken)
        {
            var entities = await _vehiculosRepository.GetVehiculos();
            var mappedEntities = _mapper.Map<List<VsWtVehiculosDto>>(entities);
            return Result<List<VsWtVehiculosDto>>.Success(mappedEntities);
        }


    }
}
