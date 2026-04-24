using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Domain.Entities.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Temas.Commands.UpdateTema
{
    public record UpdateTemaCommand : IRequest<Result<int>>
    {
        public int Id { get; init; }
        public string Titulo { get; init; } = string.Empty;
        public string Descripcion { get; init; } = string.Empty;
        public string Estado { get; init; } = string.Empty;
        public DateTime? FechaLimite { get; init; }
        public int IdDepartamentoOrigen { get; init; }
    }

    internal class UpdateTemaCommandHandler : IRequestHandler<UpdateTemaCommand, Result<int>>
    {
        private readonly ITemaRepository _temaRepository;

        public UpdateTemaCommandHandler(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        public async Task<Result<int>> Handle(UpdateTemaCommand request, CancellationToken cancellationToken)
        {
            var tema = new Tema
            {
                Id = request.Id,
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Estado = request.Estado,
                FechaLimite = request.FechaLimite,
                IdDepartamentoOrigen = request.IdDepartamentoOrigen
            };
            await _temaRepository.UpdateAsync(tema);
            return Result<int>.Success(request.Id);
        }
    }
}
