

using AutoMapper;
using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Transparencia.Queries.GetFormatoById
{

    public record GetFormatoByUserIdQuery: IRequest<Result<List<GetFormatoByUserIdDto>>>
    {
        public int Id { get; set; }

        public GetFormatoByUserIdQuery(int id)
        {
            Id = id;
        }
    }
    internal class GetFormatoByIdHandler : IRequestHandler<GetFormatoByUserIdQuery, Result<List<GetFormatoByUserIdDto>>>
    {
        private readonly ITransparenciaFormatoRepository _transparenciaFormato;
        private readonly IMapper _mapper;

        public GetFormatoByIdHandler( ITransparenciaFormatoRepository transparenciaFormato, IMapper mapper)
        {
            _transparenciaFormato = transparenciaFormato;
            _mapper = mapper;
        }

        public async Task<Result<List<GetFormatoByUserIdDto>>> Handle(GetFormatoByUserIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _transparenciaFormato.GetFormatoByUserId(request.Id);
            var mappedEntities = _mapper.Map<List<GetFormatoByUserIdDto>>(entities);
            return Result<List<GetFormatoByUserIdDto>>.Success(mappedEntities);
            
        }
    }
}
