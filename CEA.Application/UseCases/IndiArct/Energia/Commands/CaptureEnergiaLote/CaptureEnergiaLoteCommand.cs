using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Energia.Commands.CaptureEnergiaLote
{
    // Guarda en lote los valores de energía de un año.
    // Por cada item hace upsert: actualiza si ya existe (año+mes), si no inserta.
    public record CaptureEnergiaLoteCommand : IRequest<Result<int>>
    {
        public int Anio { get; init; }
        public List<EnergiaMesItem> Items { get; init; } = new();
    }

    internal class CaptureEnergiaLoteCommandHandler : IRequestHandler<CaptureEnergiaLoteCommand, Result<int>>
    {
        private readonly IIndiArctEnergiaRepository _repository;

        public CaptureEnergiaLoteCommandHandler(IIndiArctEnergiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CaptureEnergiaLoteCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || request.Items.Count == 0)
                return Result<int>.Failure("No se recibieron datos de energía para guardar");

            await _repository.UpsertLoteAsync(request.Anio, request.Items);
            return Result<int>.Success(request.Items.Count, "Energía guardada correctamente");
        }
    }
}
