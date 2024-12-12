using AutoMapper;
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.Features.Viaticos.Queries.GetAllViaticoCiudades
{
    public record GetAllViaticoCiudadesQuery : IRequest<Result<List<ViaticoCiudadDto>>>;


    internal class GetAllViaticoCiudadesQueryHandler : IRequestHandler<GetAllViaticoCiudadesQuery, Result<List<ViaticoCiudadDto>>>
    {

        private readonly IMapper _mapper;
        private readonly IViaticoCiudadRepository _viaticoCiudadRepository;

        public GetAllViaticoCiudadesQueryHandler(IMapper mapper, IViaticoCiudadRepository viaticoCiudadRepository)
        {
            _mapper = mapper;
            _viaticoCiudadRepository = viaticoCiudadRepository;
        }
        public async Task<Result<List<ViaticoCiudadDto>>> Handle(GetAllViaticoCiudadesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _viaticoCiudadRepository.GetAll();
            var viaticos = _mapper.Map<List<ViaticoCiudadDto>>(entities);
            return await Result<List<ViaticoCiudadDto>>.SuccessAsync(viaticos);
        }
    
    
    }
}
