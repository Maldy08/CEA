using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.TienePermisoParaVerOficio
{
    public record TienePermisoParaVerOficioQuery : IRequest<Result<bool>>
    {
        public int Ejercicio { get; init; }
        public int Folio { get; init; }
        public int Eor { get; init; }
        public int IdEmpleado { get; init; }

        public TienePermisoParaVerOficioQuery(int ejercicio, int folio, int eor, int idEmpleado)
        {
            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
            IdEmpleado = idEmpleado;
        }
    }

    internal class TienePermisoParaVerOficioQueryHandler : IRequestHandler<TienePermisoParaVerOficioQuery, Result<bool>>
    {
        private readonly IOficioResponsableRepository _oficioResponsableRepository;

        public TienePermisoParaVerOficioQueryHandler(IOficioResponsableRepository oficioResponsableRepository)
        {
            _oficioResponsableRepository = oficioResponsableRepository;
        }

        public async Task<Result<bool>> Handle(TienePermisoParaVerOficioQuery request, CancellationToken cancellationToken)
        {
            var tienePermiso = await _oficioResponsableRepository.TienePermisoParaVerOficio(
                request.Ejercicio, 
                request.Folio, 
                request.Eor, 
                request.IdEmpleado);

            return await Result<bool>.SuccessAsync(tienePermiso);
        }
    }
}
