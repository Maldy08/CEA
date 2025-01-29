using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetListadoOficiosFunction
{

    public record GetListadoOficiosFunctionQuery : IRequest<Result<List<OficioDtoFunction>>>
    {

        public int Ejercicio { get; set; } 
        public int Eor { get; set; }
        public int IdEmpleado { get; set; }


        public GetListadoOficiosFunctionQuery(int ejercicio, int eor, int idEmpleado)
        {
            Ejercicio = ejercicio;
            Eor = eor;
            IdEmpleado = idEmpleado;
        }


    }

    internal class GetListadoOficiosFunctionQueryHandler : IRequestHandler<GetListadoOficiosFunctionQuery, Result<List<OficioDtoFunction>>>
    {

        private readonly IOficioRepository _oficioRepository;

        public GetListadoOficiosFunctionQueryHandler(IOficioRepository oficioRepository)
        {
            _oficioRepository = oficioRepository;
        }


        public async Task<Result<List<OficioDtoFunction>>> Handle(GetListadoOficiosFunctionQuery request, CancellationToken cancellationToken)
        {
           var entities = await _oficioRepository.GetListadoOficioFunction(request.Ejercicio, request.Eor, request.IdEmpleado);
            return await Result<List<OficioDtoFunction>>.SuccessAsync(entities);
        }
    }
}
