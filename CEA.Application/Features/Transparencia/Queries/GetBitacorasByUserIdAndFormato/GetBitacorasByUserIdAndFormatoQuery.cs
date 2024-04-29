

using AutoMapper;
using CEA.Application.DTOs.Transparencia;
using CEA.Application.Features.Transparencia.Queries.GetBitacorasByUserId;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Transparencia.Queries.GetBitacorasByUserIdAndFormato
{
    public record GetBitacorasByUserIdAndFormatoQuery : IRequest<Result<List<GetBitacorasByUserIdDto>>>
    {
        public int Id { get; set; }
        public string Formato { get; set; }

        public GetBitacorasByUserIdAndFormatoQuery(int id, string formato)
        {
            Id = id;
            Formato = formato;
        }
    }
    internal class GetBitacorasByUserIdAndFormatoHandler : IRequestHandler<GetBitacorasByUserIdAndFormatoQuery, Result<List<GetBitacorasByUserIdDto>>>
    {
        private readonly ITransparenciaBitachoraArchivoRepository _transparenciaFormato;
        private readonly IMapper _mapper;

        public GetBitacorasByUserIdAndFormatoHandler(ITransparenciaBitachoraArchivoRepository transparenciaFormato, IMapper mapper)
        {
            _transparenciaFormato = transparenciaFormato ?? throw new ArgumentNullException(nameof(transparenciaFormato));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<GetBitacorasByUserIdDto>>> Handle(GetBitacorasByUserIdAndFormatoQuery request, CancellationToken cancellationToken)
        {
            var entities = await _transparenciaFormato.GetBitacorasByUserIdAndFormato(request.Id, request.Formato);
            var mappedEntities = _mapper.Map<List<GetBitacorasByUserIdDto>>(entities);
            return Result<List<GetBitacorasByUserIdDto>>.Success(mappedEntities);
        }
    }
}
