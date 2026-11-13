using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetSaldoVacaciones
{
    public record GetSaldoVacacionesQuery : IRequest<Result<SaldoVacacionesDto>>
    {
        public int NoEmp { get; init; }

        public GetSaldoVacacionesQuery(int noEmp)
        {
            NoEmp = noEmp;
        }
    }
    internal class GetSaldoVacacionesQueryHandler : IRequestHandler<GetSaldoVacacionesQuery, Result<SaldoVacacionesDto>>
    {
        private readonly INomVacacionesFunctions _nomVacacionesFunctions;

        public GetSaldoVacacionesQueryHandler(INomVacacionesFunctions nomVacacionesFunctions)
        {
            _nomVacacionesFunctions = nomVacacionesFunctions;
        }

        public async Task<Result<SaldoVacacionesDto>> Handle(GetSaldoVacacionesQuery request, CancellationToken cancellationToken)
        {
            var saldo = await _nomVacacionesFunctions.SaldoVacaciones(request.NoEmp);
            if (saldo == null)
            {
                return await Result<SaldoVacacionesDto>.FailureAsync($"El empleado {request.NoEmp} no existe o no está activo.");
            }
            return await Result<SaldoVacacionesDto>.SuccessAsync(saldo);
        }
    }
}
