using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficiosClasificacionQuery
{

    public record class GetOficiosClasificacionQuery : IRequest<Result<List<OficioClasificacionDto>>>
    {


    }
    internal class GetOficiosClasificacionQueryHandler : IRequestHandler<GetOficiosClasificacionQuery, Result<List<OficioClasificacionDto>>>
    {

        private readonly IOficioRepository _oficioRepository;
        private readonly IMapper _mapper;

        public GetOficiosClasificacionQueryHandler(IOficioRepository oficioRepository, IMapper mapper)
        {
            _oficioRepository = oficioRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OficioClasificacionDto>>> Handle(GetOficiosClasificacionQuery request, CancellationToken cancellationToken)
        {
            var entidades = await _oficioRepository.GetOficioClasificacions();
            var entidadesDto = _mapper.Map<List<OficioClasificacionDto>>(entidades);
            return await Result<List<OficioClasificacionDto>>.SuccessAsync(entidadesDto);
        }
    }
}
