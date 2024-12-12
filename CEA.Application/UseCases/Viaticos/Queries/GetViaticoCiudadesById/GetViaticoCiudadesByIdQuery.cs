using AutoMapper;
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.Features.Viaticos.Queries.GetViaticoCiudadesById
{
    public record GetViaticoCiudadesByIdQuery : IRequest<Result<ViaticoCiudadDto>>
    {
        public int Id { get; set; }

        public GetViaticoCiudadesByIdQuery(int id)
        {
            Id = id;
        }
    }
    internal class GetViaticoCiudadesByIdQueryHandler : IRequestHandler<GetViaticoCiudadesByIdQuery, Result<ViaticoCiudadDto>>
    {
        private readonly IMapper _mapper;
        private readonly IViaticoCiudadRepository _viaticoCiudadRepository;

        public GetViaticoCiudadesByIdQueryHandler(IMapper mapper, IViaticoCiudadRepository viaticoCiudadRepository)
        {
            _mapper = mapper;
            _viaticoCiudadRepository = viaticoCiudadRepository;
        }

        public async Task<Result<ViaticoCiudadDto>> Handle(GetViaticoCiudadesByIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _viaticoCiudadRepository.GetById(request.Id);
            var viaticos = _mapper.Map<ViaticoCiudadDto>(entities);
            return await Result<ViaticoCiudadDto>.SuccessAsync(viaticos);
        }
    }
}
