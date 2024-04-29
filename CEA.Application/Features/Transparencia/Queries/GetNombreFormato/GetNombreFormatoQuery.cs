
using AutoMapper;
using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories.Transparencia;
using MediatR;

namespace CEA.Application.Features.Transparencia.Queries.GetNombreFormato
{
    public record GetNombreFormatoQuery : IRequest<GetNombreFormatoDto>
    {
        public string NombreFormato { get; init; } = string.Empty;

        public GetNombreFormatoQuery(string nombreFormato)
        {
            NombreFormato = nombreFormato;
        }
    }
    internal class GetNombreFormatoHandler : IRequestHandler<GetNombreFormatoQuery, GetNombreFormatoDto>
    {
        private readonly ITransparenciaFormatoRepository _transparenciaFormato;
        private readonly IMapper _mapper;

        public GetNombreFormatoHandler(ITransparenciaFormatoRepository transparenciaFormato, IMapper mapper)
        {
            _transparenciaFormato = transparenciaFormato;
            _mapper = mapper;
        }

        public async Task<GetNombreFormatoDto> Handle(GetNombreFormatoQuery request, CancellationToken cancellationToken)
        {
            var entity =  await _transparenciaFormato.GetNombreFormatoByUserId(request.NombreFormato);
            var mappedEntity = _mapper.Map<GetNombreFormatoDto>(entity);
            return mappedEntity;
        }
    }
}
