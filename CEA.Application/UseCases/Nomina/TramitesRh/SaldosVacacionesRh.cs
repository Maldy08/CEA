using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Domain.Entities.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.TramitesRh
{
    // Saldos de vacaciones capturados por RH (TRAMITES_NIVEL 9): el punto de partida de F_SALDO_VACACIONES.
    // La función toma el renglón de NOM_VACAEMP_M/_D con FHASTA más reciente del empleado y suma desde ahí; sin
    // ninguno, cuenta desde el año de alta. RH captura "último periodo + días que le quedan" y se
    // guarda como M (ORIGEN_REGISTRO 'R', sin trámite) + un D. F_SALDO no cambia. La fecha (FDESDE = FHASTA) es la del
    // día de la captura: solo sirve para que F_SALDO la tome como el registro más reciente.

    public record GetSaldosVacacionesRhQuery(int NoEmpActor) : IRequest<Result<List<SaldoVacacionesRhDto>>>;

    public record GetRegistrosVacacionesRhQuery(int NoEmpActor, int NoEmp) : IRequest<Result<List<RegistroVacacionesRhDto>>>;

    // PerVac 'AAAA-N' (N = 1 o 2): último periodo del que se tiene el dato; DiasXDisf: lo que le queda de ese periodo
    // (0 = ya lo gastó todo).
    public record CreateSaldoInicialVacacionesRhCommand : IRequest<Result<SaldoVacacionesDto?>>
    {
        public int NoEmpActor { get; init; }
        public int NoEmp { get; init; }
        public string PerVac { get; init; } = string.Empty;
        public int DiasXDisf { get; init; }
    }

    public record DeleteRegistroVacacionesRhCommand(int NoEmpActor, int IdVacaemp) : IRequest<Result<int>>;

    internal class GetSaldosVacacionesRhQueryHandler : IRequestHandler<GetSaldosVacacionesRhQuery, Result<List<SaldoVacacionesRhDto>>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;

        public GetSaldosVacacionesRhQueryHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones)
        {
            _tramites = tramites;
            _funciones = funciones;
        }

        public async Task<Result<List<SaldoVacacionesRhDto>>> Handle(GetSaldosVacacionesRhQuery request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsAdministradorRh(request.NoEmpActor))
                return await Result<List<SaldoVacacionesRhDto>>.FailureAsync(TramitesRhMensajes.SinPermiso);
            return await Result<List<SaldoVacacionesRhDto>>.SuccessAsync(await _tramites.GetSaldosVacacionesAsync());
        }
    }

    internal class GetRegistrosVacacionesRhQueryHandler : IRequestHandler<GetRegistrosVacacionesRhQuery, Result<List<RegistroVacacionesRhDto>>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;

        public GetRegistrosVacacionesRhQueryHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones)
        {
            _tramites = tramites;
            _funciones = funciones;
        }

        public async Task<Result<List<RegistroVacacionesRhDto>>> Handle(GetRegistrosVacacionesRhQuery request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsAdministradorRh(request.NoEmpActor))
                return await Result<List<RegistroVacacionesRhDto>>.FailureAsync(TramitesRhMensajes.SinPermiso);
            return await Result<List<RegistroVacacionesRhDto>>.SuccessAsync(await _tramites.GetRegistrosVacacionesAsync(request.NoEmp));
        }
    }

    internal class CreateSaldoInicialVacacionesRhCommandHandler
        : IRequestHandler<CreateSaldoInicialVacacionesRhCommand, Result<SaldoVacacionesDto?>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;
        private readonly INomVacacionesFunctions _vacaciones;

        public CreateSaldoInicialVacacionesRhCommandHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones,
            INomVacacionesFunctions vacaciones)
        {
            _tramites = tramites;
            _funciones = funciones;
            _vacaciones = vacaciones;
        }

        public async Task<Result<SaldoVacacionesDto?>> Handle(CreateSaldoInicialVacacionesRhCommand request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsAdministradorRh(request.NoEmpActor))
                return await Result<SaldoVacacionesDto?>.FailureAsync(TramitesRhMensajes.SinPermiso);

            // F_SALDO regresa nulo si el empleado no existe o no está activo; de ahí salen alta y antigüedad.
            var empleado = await _vacaciones.SaldoVacaciones(request.NoEmp);
            if (empleado == null) return await Result<SaldoVacacionesDto?>.FailureAsync("El empleado no existe o no está activo.");

            var partes = (request.PerVac ?? "").Trim().Split('-');
            if (partes.Length != 2 || partes[0].Length != 4 || !int.TryParse(partes[0], out var ano)
                || !int.TryParse(partes[1], out var periodo) || (periodo != 1 && periodo != 2))
                return await Result<SaldoVacacionesDto?>.FailureAsync("El periodo debe tener la forma AAAA-1 o AAAA-2.");

            // Mismo rango que F_SALDO: del año de alta al año en que cumple su antigüedad actual.
            var anoAlta = empleado.FechaAlta.Year;
            var anoMax = anoAlta + empleado.AntiguedadActualAnos;
            if (ano < anoAlta || ano > anoMax)
                return await Result<SaldoVacacionesDto?>.FailureAsync(
                    $"El periodo debe estar entre {anoAlta} (año de alta) y {anoMax} (antigüedad actual de {empleado.AntiguedadActualAnos} años).");

            var tope = await _tramites.DiasPorPeriodoAsync(ano - anoAlta) ?? 0;
            if (request.DiasXDisf < 0 || request.DiasXDisf > tope)
                return await Result<SaldoVacacionesDto?>.FailureAsync(
                    $"Los días por disfrutar del periodo {ano}-{periodo} van de 0 a {tope:0.#} (tabulador con {ano - anoAlta} años de antigüedad).");

            var corte = DateTime.Today;

            // F_SALDO usa el registro con FHASTA más reciente: una solicitud que termina hoy o después taparía esta captura.
            var posterior = await _tramites.GetFolioVacacionesPosteriorAsync(request.NoEmp, corte);
            if (posterior != null)
                return await Result<SaldoVacacionesDto?>.FailureAsync(
                    $"El empleado tiene la solicitud {posterior}, que termina hoy o después; el saldo se calcula a partir de ella. Cancélela o modifíquela antes de capturar el saldo.");

            await _tramites.CreateRegistroRhAsync(
                new NomVacaempM
                {
                    OrigenRegistro = NomVacaempM.OrigenRh,
                    Ano = corte.Year,
                    NoEmp = request.NoEmp,
                    DiasSolicitados = 0,
                    FDesde = corte,
                    FHasta = corte,
                    IdEmpleadoCaptura = request.NoEmpActor,
                    FechaCaptura = DateTime.Now
                },
                new NomVacaempD
                {
                    NoEmp = request.NoEmp,
                    Ano = ano,
                    PerVac = $"{ano}-{periodo}",
                    DiasVac = 0,
                    DiasXDisf = request.DiasXDisf
                },
                cancellationToken);

            return await Result<SaldoVacacionesDto?>.SuccessAsync(await _vacaciones.SaldoVacaciones(request.NoEmp), "Saldo registrado.");
        }
    }

    internal class DeleteRegistroVacacionesRhCommandHandler : IRequestHandler<DeleteRegistroVacacionesRhCommand, Result<int>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;

        public DeleteRegistroVacacionesRhCommandHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones)
        {
            _tramites = tramites;
            _funciones = funciones;
        }

        public async Task<Result<int>> Handle(DeleteRegistroVacacionesRhCommand request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsAdministradorRh(request.NoEmpActor))
                return await Result<int>.FailureAsync(TramitesRhMensajes.SinPermiso);
            return await _tramites.DeleteRegistroRhAsync(request.IdVacaemp, cancellationToken)
                ? await Result<int>.SuccessAsync(request.IdVacaemp, "Registro eliminado.")
                : await Result<int>.FailureAsync("El registro no existe o no fue capturado por RH (los de trámites se cancelan desde el trámite).");
        }
    }
}
