using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.GetEsEmpleadoResponsable
{
    public record GetEsEmpleadoResponsableQuery : IRequest<Result<bool>>
    {
        public int IdEmpleado { get; set; }
        public GetEsEmpleadoResponsableQuery(int idEmpleado)
        {
            IdEmpleado = idEmpleado;
        }
    }

    internal class GetEsEmpleadoResponsableQueryHandler : IRequestHandler<GetEsEmpleadoResponsableQuery, Result<bool>>
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public GetEsEmpleadoResponsableQueryHandler(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        public async Task<Result<bool>> Handle(GetEsEmpleadoResponsableQuery request, CancellationToken cancellationToken)
        {
            var esResponsable = await _empleadoRepository.EsEmpleadoResponsableAsync(request.IdEmpleado);
            return Result<bool>.Success(esResponsable);
        }
    }
}
