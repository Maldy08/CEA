
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficiosMCByEjercicio
{
    public record GetOficiosMCByEjercicioQuery : IRequest<Result<List<Oficio>>>
    {
        public int Eor { get; set; }
        public int Ejercicio { get; set; }

        public GetOficiosMCByEjercicioQuery(int eor, int ejercicio)
        {
            Eor = eor;
            Ejercicio = ejercicio;
        }

        internal class GetOficiosMCByEjercicioQueryHandler : IRequestHandler<GetOficiosMCByEjercicioQuery, Result<List<Oficio>>>
        {

            private readonly IMediator _mediator;
            private readonly IOficioRepository _oficioRepository;

            public GetOficiosMCByEjercicioQueryHandler(IMediator mediator, IOficioRepository oficioRepository)
            {
                _mediator = mediator;
                _oficioRepository = oficioRepository;
            }

            public async Task<Result<List<Oficio>>> Handle(GetOficiosMCByEjercicioQuery request, CancellationToken cancellationToken)
            {
               var oficios = await _oficioRepository.GetOficiosMCByEjercicio(request.Eor, request.Ejercicio);
                foreach (var oficio in oficios)
                {
                    oficio.OficiosResponsables.Add(new OficioResponsable { Ejercicio = 2024, Eor = 2, Folio = 1, IdEmpleado = 7148, Rol = 1 });
                }
                return await Result<List<Oficio>>.SuccessAsync(oficios);
            }
        }
    }
}
