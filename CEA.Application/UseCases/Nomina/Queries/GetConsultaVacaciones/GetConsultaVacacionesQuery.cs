using System.Globalization;
using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetConsultaVacaciones
{
    public record GetConsultaVacacionesQuery : IRequest<Result<ConsultaVacacionesDto>>
    {
        public int NoEmp { get; init; }
        public DateTime Desde { get; init; }
        public DateTime Hasta { get; init; }

        public GetConsultaVacacionesQuery(int noEmp, DateTime desde, DateTime hasta)
        {
            NoEmp = noEmp;
            Desde = desde;
            Hasta = hasta;
        }
    }
    internal class GetConsultaVacacionesQueryHandler : IRequestHandler<GetConsultaVacacionesQuery, Result<ConsultaVacacionesDto>>
    {
        private readonly INomVacacionesFunctions _nomVacacionesFunctions;

        public GetConsultaVacacionesQueryHandler(INomVacacionesFunctions nomVacacionesFunctions)
        {
            _nomVacacionesFunctions = nomVacacionesFunctions;
        }

        public async Task<Result<ConsultaVacacionesDto>> Handle(GetConsultaVacacionesQuery request, CancellationToken cancellationToken)
        {
            if (request.Hasta < request.Desde)
            {
                return await Result<ConsultaVacacionesDto>.FailureAsync("La fecha final no puede ser anterior a la inicial.");
            }
            var renglones = await _nomVacacionesFunctions.ConsultaVacaciones(request.NoEmp, request.Desde, request.Hasta);
            if (renglones.Count < 2)
            {
                return await Result<ConsultaVacacionesDto>.FailureAsync("El cálculo de vacaciones no regresó resultado.");
            }

            // El renglón 2 trae los festivos o, si algo falló, el mensaje de la función.
            var festivos = renglones[1];
            if (festivos != null && (festivos.StartsWith("ERROR") || festivos == "EMPLEADO NO EXISTE"))
            {
                return await Result<ConsultaVacacionesDto>.FailureAsync(festivos);
            }

            try
            {
                return await Result<ConsultaVacacionesDto>.SuccessAsync(new ConsultaVacacionesDto
                {
                    DiasHabiles = (int)Numero(renglones[0]),
                    Festivos = string.IsNullOrWhiteSpace(festivos) ? null : festivos,
                    Periodos = renglones.Skip(2).Where(r => !string.IsNullOrWhiteSpace(r)).Select(Periodo).ToList()
                });
            }
            catch (FormatException)
            {
                return await Result<ConsultaVacacionesDto>.FailureAsync("El cálculo de vacaciones regresó un formato no válido.");
            }
        }

        // "NOEMP|ANO|DIAS|PERIODO|DIASXDISFRUTAR"
        private static SimulaVacacionesDto Periodo(string? renglon)
        {
            var c = renglon!.Split('|');
            if (c.Length != 5) throw new FormatException();
            return new SimulaVacacionesDto
            {
                NoEmp = (int)Numero(c[0]),
                Ano = (int)Numero(c[1]),
                Dias = Numero(c[2]),
                Periodo = (int)Numero(c[3]),
                DiasXDisfrutar = Numero(c[4])
            };
        }

        // Oracle convierte el número a texto con el separador decimal de la sesión: acepta punto o coma.
        private static decimal Numero(string? valor) =>
            decimal.Parse((valor ?? "").Trim().Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture);
    }
}
