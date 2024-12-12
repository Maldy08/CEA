

using AutoMapper;
using CEA.Application.DTOs.Vehiculos;
using CEA.Application.Interfaces.Repositories.Vehiculos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Vehiculos.Queries.GetVehiculoByNoEconomico
{
    public class GetVehiculoByNoEconomicoQuery : IRequest<Result<VsWtVehiculosDto>>
    {
        public int NoEconomico { get; set; }

        public GetVehiculoByNoEconomicoQuery(int noEconomico)
        {
            NoEconomico = noEconomico;
        }
    }
    internal class GetVehiculoByNoEconomicoQueryHandler : IRequestHandler<GetVehiculoByNoEconomicoQuery, Result<VsWtVehiculosDto>>
    {
        private readonly IVsWtVehiculosRepository _vehiculosRepository;
        private readonly IMapper _mapper;

        public GetVehiculoByNoEconomicoQueryHandler(IVsWtVehiculosRepository vehiculosRepository, IMapper mapper)
        {
            _vehiculosRepository = vehiculosRepository;
            _mapper = mapper;
        }
        public async Task<Result<VsWtVehiculosDto>> Handle(GetVehiculoByNoEconomicoQuery request, CancellationToken cancellationToken)
        {
          var vehiculo = await _vehiculosRepository.GetVehiculoByNoEconomico(request.NoEconomico);
          var mappedVehiculo = _mapper.Map<VsWtVehiculosDto>(vehiculo);
          return Result<VsWtVehiculosDto>.Success(mappedVehiculo);
        }
    }
}
