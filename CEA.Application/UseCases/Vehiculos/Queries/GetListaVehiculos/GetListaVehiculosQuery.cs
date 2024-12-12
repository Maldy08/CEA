using AutoMapper;
using CEA.Application.DTOs.Vehiculos;
using CEA.Application.Interfaces.Repositories.Vehiculos;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.Features.Vehiculos.Queries.GetListaVehiculos
{

    public record GetListaVehiculosQuery : IRequest<Result<List<VsListaVehiculosDto>>>
    {

        public GetListaVehiculosQuery()
        {
            
        }
    }


    internal class GetListaVehiculosQueryHandler : IRequestHandler<GetListaVehiculosQuery, Result<List<VsListaVehiculosDto>>>
    {

        private readonly IVsListaVehiculosRepository _vsListaVehiculosRepository;
        private readonly IMapper _mapper;

        public GetListaVehiculosQueryHandler(IVsListaVehiculosRepository vsListaVehiculosRepository, IMapper mapper)
        {
            _vsListaVehiculosRepository = vsListaVehiculosRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<VsListaVehiculosDto>>> Handle(GetListaVehiculosQuery request, CancellationToken cancellationToken)
        {
           var result = await _vsListaVehiculosRepository.GetAllVehiculos();
           var entity = _mapper.Map<List<VsListaVehiculosDto>>(result);
           return Result<List<VsListaVehiculosDto>>.Success(entity);

        }
    
    
    }
}
