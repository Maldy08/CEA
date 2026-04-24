using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Domain.Entities.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Involucrados.Commands.AssignInvolucrado
{
    public record AssignInvolucradoCommand : IRequest<Result<int>>
    {
        public int IdTema { get; init; }
        public int IdUsuario { get; init; }
        public string TipoInvolucrado { get; init; } = "Visualizador";
    }

    internal class AssignInvolucradoCommandHandler : IRequestHandler<AssignInvolucradoCommand, Result<int>>
    {
        private readonly ITemaInvolucradoRepository _involucradoRepository;

        public AssignInvolucradoCommandHandler(ITemaInvolucradoRepository involucradoRepository)
        {
            _involucradoRepository = involucradoRepository;
        }

        public async Task<Result<int>> Handle(AssignInvolucradoCommand request, CancellationToken cancellationToken)
        {
            var involucrado = new TemaInvolucrado
            {
                IdTema = request.IdTema,
                IdUsuario = request.IdUsuario,
                TipoInvolucrado = request.TipoInvolucrado,
                FechaAsignacion = DateTime.Now
            };
            var created = await _involucradoRepository.AddAsync(involucrado);
            return Result<int>.Success(created.Id);
        }
    }
}
