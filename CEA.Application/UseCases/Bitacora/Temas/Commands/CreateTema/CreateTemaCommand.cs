using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Domain.Entities.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Temas.Commands.CreateTema
{
    public record CreateTemaCommand : IRequest<Result<int>>
    {
        public string Titulo { get; init; } = string.Empty;
        public string Descripcion { get; init; } = string.Empty;
        public string Estado { get; init; } = "Pendiente";
        public DateTime? FechaLimite { get; init; }
        public int IdDepartamentoOrigen { get; init; }
    }

    internal class CreateTemaCommandHandler : IRequestHandler<CreateTemaCommand, Result<int>>
    {
        private readonly ITemaRepository _temaRepository;

        public CreateTemaCommandHandler(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        public async Task<Result<int>> Handle(CreateTemaCommand request, CancellationToken cancellationToken)
        {
            var tema = new Tema
            {
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Estado = request.Estado,
                FechaCreacion = DateTime.Now,
                FechaLimite = request.FechaLimite,
                IdDepartamentoOrigen = request.IdDepartamentoOrigen
            };
            var created = await _temaRepository.AddAsync(tema);
            return Result<int>.Success(created.Id);
        }
    }
}
