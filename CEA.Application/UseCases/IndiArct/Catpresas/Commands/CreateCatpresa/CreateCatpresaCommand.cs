using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Domain.Entities.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Catpresas.Commands.CreateCatpresa
{
    public record CreateCatpresaCommand : IRequest<Result<int>>
    {
        public int Id { get; init; } // ID_PRESA asignado manualmente
        public string NombreOficial { get; init; } = string.Empty;
        public string Municipio { get; init; } = string.Empty;
        public decimal Latitud { get; init; }
        public decimal Longitud { get; init; }
        public string? CorrientePrincipal { get; init; }
        public decimal? CapacidadNamoHm3 { get; init; }
        public string? UsoPrincipal { get; init; }
    }

    internal class CreateCatpresaCommandHandler : IRequestHandler<CreateCatpresaCommand, Result<int>>
    {
        private readonly IIndiArctCatpresasRepository _repository;

        public CreateCatpresaCommandHandler(IIndiArctCatpresasRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateCatpresaCommand request, CancellationToken cancellationToken)
        {
            var entity = new IndiArctCatpresas
            {
                Id = request.Id,
                NombreOficial = request.NombreOficial,
                Municipio = request.Municipio,
                Latitud = request.Latitud,
                Longitud = request.Longitud,
                CorrientePrincipal = request.CorrientePrincipal,
                CapacidadNamoHm3 = request.CapacidadNamoHm3,
                UsoPrincipal = request.UsoPrincipal
            };
            var created = await _repository.AddAsync(entity);
            return Result<int>.Success(created.Id);
        }
    }
}
