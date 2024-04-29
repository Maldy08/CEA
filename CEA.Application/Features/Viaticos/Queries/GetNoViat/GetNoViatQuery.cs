using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Queries.GetNoViat
{

    public record GetNoViatQuery : IRequest<Result<int>>
    {
        public int Ejercicio { get; set; }
        public int Oficina { get; set; }

        public GetNoViatQuery(int ejercicio, int oficina)
        {
            Ejercicio = ejercicio;
            Oficina = oficina;
        }
    }
    internal class GetNoViatQueryHandler: IRequestHandler<GetNoViatQuery, Result<int>>
    {
        private readonly IViaticoRepository _viaticoRepository;

        public GetNoViatQueryHandler(IViaticoRepository viaticoRepository)
        {
            _viaticoRepository = viaticoRepository;
        }
        public async Task<Result<int>> Handle(GetNoViatQuery query, CancellationToken cancellationToken)
        {
            var result = await _viaticoRepository.GetNoViat(query.Ejercicio, query.Oficina);
            return await Result<int>.SuccessAsync(result);
        }
    }
}
