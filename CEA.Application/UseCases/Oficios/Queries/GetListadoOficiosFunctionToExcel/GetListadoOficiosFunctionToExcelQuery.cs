using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetListadoOficiosFunctionToExcel
{

    public record GetListadoOficiosFunctionToExcelQuery : IRequest<Result<List<OficioDtoFunction>>>
    {
        public int Ejercicio { get; set; }
        public int IdEmpleado { get; set; }

        public GetListadoOficiosFunctionToExcelQuery(int ejercicio, int idEmpleado)
        {
            Ejercicio = ejercicio;
            IdEmpleado = idEmpleado;
        }
    }
    internal class GetListadoOficiosFunctionToExcelQueryHandler : IRequestHandler<GetListadoOficiosFunctionToExcelQuery, Result<List<OficioDtoFunction>>>
    {


        private readonly IOficioRepository _oficioRepository;

        public GetListadoOficiosFunctionToExcelQueryHandler(IOficioRepository oficioRepository)
        {
            _oficioRepository = oficioRepository;
        }

        public async Task<Result<List<OficioDtoFunction>>> Handle(GetListadoOficiosFunctionToExcelQuery request, CancellationToken cancellationToken)
        {
            var expedidos = await _oficioRepository.GetListadoOficioFunction(request.Ejercicio, 1, request.IdEmpleado);
            var xexpedir = await _oficioRepository.GetListadoOficioFunction(request.Ejercicio, 3, request.IdEmpleado);

            var entities = expedidos.Union(xexpedir).ToList();
            return await Result<List<OficioDtoFunction>>.SuccessAsync(entities);
        }
    }
}
