using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.PresasNiveles.Commands.CapturePresasLote
{
    // Guarda en lote los volúmenes capturados para un año/mes.
    // Por cada item hace upsert: actualiza si ya existe (año+mes+presa), si no inserta.
    public record CapturePresasLoteCommand : IRequest<Result<int>>
    {
        public int Anio { get; init; }
        public int Mes { get; init; }
        public List<PresaVolumenItem> Items { get; init; } = new();
    }

    internal class CapturePresasLoteCommandHandler : IRequestHandler<CapturePresasLoteCommand, Result<int>>
    {
        private readonly IIndiArctPresasNivelesRepository _repository;

        public CapturePresasLoteCommandHandler(IIndiArctPresasNivelesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CapturePresasLoteCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || request.Items.Count == 0)
                return Result<int>.Failure("No se recibieron volúmenes para guardar");

            await _repository.UpsertLoteAsync(request.Anio, request.Mes, request.Items);
            return Result<int>.Success(request.Items.Count, "Capturas guardadas correctamente");
        }
    }
}
