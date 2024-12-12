
using AutoMapper;
using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Transparencia.Queries.GetBitacorasByUserId
{
    public record GetBitacorasByUserIdQuery : IRequest<Result<List<GetBitacorasByUserIdDto>>>
    {
        public int Id { get; set; }

        public GetBitacorasByUserIdQuery(int id)
        {
            Id = id;
        }
    
    }
    internal class GetBitacorasByUserIdHandler : IRequestHandler<GetBitacorasByUserIdQuery, Result<List<GetBitacorasByUserIdDto>>>
    {

        private readonly ITransparenciaBitachoraArchivoRepository _transparenciaFormato;
        private readonly IMapper _mapper;

        public GetBitacorasByUserIdHandler(ITransparenciaBitachoraArchivoRepository transparenciaFormato, IMapper mapper)
        {
            _transparenciaFormato = transparenciaFormato ?? throw new ArgumentNullException(nameof(transparenciaFormato));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<GetBitacorasByUserIdDto>>> Handle(GetBitacorasByUserIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _transparenciaFormato.GetBitacorasByUserId(request.Id);
            var mappedEntities = _mapper.Map<List<GetBitacorasByUserIdDto>>(entities);
            return Result<List<GetBitacorasByUserIdDto>>.Success(mappedEntities);
        }
    }
}
