

using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Oficios.Queries.GetOficiosEstatusByIdEor
{

    public record GetOficiosEstatusByIdEorQuery : IRequest<Result<OficioEstatusDto>>
    {
        public int Id { get; set; }
        public int Eor { get; set; }

        public GetOficiosEstatusByIdEorQuery(int id, int eor)
        {
            Id = id;
            Eor = eor;
        }

    }
    internal class GetOficiosEstatusByIdEorQueryHandler : IRequestHandler<GetOficiosEstatusByIdEorQuery, Result<OficioEstatusDto>>
    {
        private readonly IOficioEstatusRepository _oficioEstatusRepository;
        private readonly IMapper _mapper;

       public GetOficiosEstatusByIdEorQueryHandler(IOficioEstatusRepository oficioEstatusRepository, IMapper mapper)
        {
            _oficioEstatusRepository = oficioEstatusRepository;
            _mapper = mapper;
        }

        public async Task<Result<OficioEstatusDto>> Handle(GetOficiosEstatusByIdEorQuery request, CancellationToken cancellationToken)
        {
           var entities = await _oficioEstatusRepository.GetEstatusByIdEor(request.Id, request.Eor);
            var mappedEntities = _mapper.Map<OficioEstatusDto>(entities);
            return await Result<OficioEstatusDto>.SuccessAsync(mappedEntities);
        }
    }
}
