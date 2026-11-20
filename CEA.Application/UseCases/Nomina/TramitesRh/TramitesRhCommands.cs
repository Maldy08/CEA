using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Application.UseCases.Nomina.Commands.CreatePermisoJustificante;
using CEA.Application.UseCases.Nomina.Commands.CreateSolicitudVacaciones;
using CEA.Application.UseCases.Nomina.Queries.GetConsultaVacaciones;
using CEA.Domain.Entities.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.TramitesRh
{
    // Acciones del administrador de RH sobre cualquier trámite de NOM_TRAMITES (sin viáticos), en cualquier estatus.
    // Todas dejan movimiento en la bitácora con el número de quien las hizo. NoEmpActor lo pone el controlador.

    // Completar: RH autoriza (100, FECHA_AUTORIZACION) en cuanto el empleado lo envió a firma, aunque falten las
    // firmas del jefe o del director: Revisión Jefe (2), Revisión Director (3) o Revisión RH (4). No desde Creado (1)
    // ni Regresado (98): están en manos del empleado.
    public record CompletarTramiteRhCommand : IRequest<Result<int>>
    {
        public int IdTramite { get; init; }
        public int NoEmpActor { get; init; }
        public string? Comentarios { get; init; }
    }

    // Cancelar en cualquier estatus (menos ya cancelado). En vacaciones borra NOM_VACAEMP_D: los días vuelven al saldo.
    public record CancelarTramiteRhCommand : IRequest<Result<int>>
    {
        public int IdTramite { get; init; }
        public int NoEmpActor { get; init; }
        public string Comentarios { get; init; } = string.Empty;
    }

    // Modificar vacaciones: rehace días y reparto por periodo con FN_CONSULTA_VACACIONES sin contar los días del propio trámite.
    public record ModificarVacacionesRhCommand : IRequest<Result<int>>
    {
        public int IdTramite { get; init; }
        public int NoEmpActor { get; init; }
        public DateTime Desde { get; init; }
        public DateTime Hasta { get; init; }
        public DateTime? FechaRegreso { get; init; }
        public int? IdEmpleadoCubre { get; init; }
        // Motivo de la modificación (obligatorio, va a la bitácora).
        public string Comentarios { get; init; } = string.Empty;
    }

    // Modificar permiso o justificante: mismas reglas que al generarlo. La incidencia puede cambiar dentro del mismo tipo
    // de trámite (un permiso sigue siendo permiso: el folio PER/JUS no cambia).
    public record ModificarPermisoJustificanteRhCommand : IRequest<Result<int>>
    {
        public int IdTramite { get; init; }
        public int NoEmpActor { get; init; }
        public int TipoIncidencia { get; init; }
        public DateTime FechaInicio { get; init; }
        public DateTime FechaFin { get; init; }
        public string JustificacionMotivo { get; init; } = string.Empty;
        public int? IdEmpleadoCubre { get; init; }
        public string Comentarios { get; init; } = string.Empty;
    }

    internal static class ReglasRh
    {
        public const int TipoViaticos = 2;
        public const int TipoVacaciones = 1;
        public const int EstatusCancelado = 99;
        public const int EstatusAutorizado = 100;
        // Estatus desde los que RH puede autorizar (el empleado ya lo envió a firma).
        public static readonly int[] Autorizables = { 2, 3, 4 };
        // Todos los estatus de RH_CAT_ESTATUS menos 99.
        public static readonly int[] Vigentes = { 1, 2, 3, 4, 5, 98, 100, 101, 110 };

        public static string? ValidarMotivo(string? comentarios, bool obligatorio, string pide)
        {
            var motivo = comentarios?.Trim() ?? "";
            if (obligatorio && motivo.Length == 0) return pide;
            if (motivo.Length > 1000) return "El motivo no puede pasar de 1000 caracteres.";
            return null;
        }
    }

    internal class CompletarTramiteRhCommandHandler : IRequestHandler<CompletarTramiteRhCommand, Result<int>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;

        public CompletarTramiteRhCommandHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones)
        {
            _tramites = tramites;
            _funciones = funciones;
        }

        public async Task<Result<int>> Handle(CompletarTramiteRhCommand request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsAdministradorRh(request.NoEmpActor))
                return await Result<int>.FailureAsync(TramitesRhMensajes.SinPermiso);
            var error = ReglasRh.ValidarMotivo(request.Comentarios, false, "");
            if (error != null) return await Result<int>.FailureAsync(error);

            var tramite = await _tramites.GetByIdAsync(request.IdTramite);
            if (tramite == null || tramite.TipoTramite == ReglasRh.TipoViaticos)
                return await Result<int>.FailureAsync("El trámite no existe.");

            var permitidos = ReglasRh.Autorizables;
            var anterior = await _tramites.CambiarEstatusAsync(request.IdTramite, permitidos, ReglasRh.EstatusAutorizado,
                new NomTramiteBitacora
                {
                    IdEmpleado = request.NoEmpActor,
                    Accion = "AUTORIZADO POR RH",
                    Comentarios = string.IsNullOrWhiteSpace(request.Comentarios) ? null : request.Comentarios.Trim()
                },
                borrarDetalleVacaciones: false, cancellationToken);
            if (anterior == null) return await Result<int>.FailureAsync("El trámite no existe.");
            if (!permitidos.Contains(anterior.Value))
                return await Result<int>.FailureAsync(anterior == ReglasRh.EstatusAutorizado
                    ? "El trámite ya estaba autorizado."
                    : anterior == ReglasRh.EstatusCancelado
                        ? "El trámite está cancelado; ya no se puede autorizar."
                        : "Solo se autorizan trámites que el empleado ya envió a firma (en revisión del jefe, director o RH).");
            return await Result<int>.SuccessAsync(ReglasRh.EstatusAutorizado, "Trámite autorizado.");
        }
    }

    internal class CancelarTramiteRhCommandHandler : IRequestHandler<CancelarTramiteRhCommand, Result<int>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;

        public CancelarTramiteRhCommandHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones)
        {
            _tramites = tramites;
            _funciones = funciones;
        }

        public async Task<Result<int>> Handle(CancelarTramiteRhCommand request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsAdministradorRh(request.NoEmpActor))
                return await Result<int>.FailureAsync(TramitesRhMensajes.SinPermiso);
            var error = ReglasRh.ValidarMotivo(request.Comentarios, true, "Escriba el motivo de la cancelación.");
            if (error != null) return await Result<int>.FailureAsync(error);

            var tramite = await _tramites.GetByIdAsync(request.IdTramite);
            if (tramite == null || tramite.TipoTramite == ReglasRh.TipoViaticos)
                return await Result<int>.FailureAsync("El trámite no existe.");

            var anterior = await _tramites.CambiarEstatusAsync(request.IdTramite, ReglasRh.Vigentes, ReglasRh.EstatusCancelado,
                new NomTramiteBitacora { IdEmpleado = request.NoEmpActor, Accion = "CANCELADO POR RH", Comentarios = request.Comentarios.Trim() },
                borrarDetalleVacaciones: tramite.TipoTramite == ReglasRh.TipoVacaciones, cancellationToken);
            if (anterior == null) return await Result<int>.FailureAsync("El trámite no existe.");
            if (anterior == ReglasRh.EstatusCancelado) return await Result<int>.FailureAsync("El trámite ya estaba cancelado.");
            return await Result<int>.SuccessAsync(ReglasRh.EstatusCancelado, "Trámite cancelado.");
        }
    }

    internal class ModificarVacacionesRhCommandHandler : IRequestHandler<ModificarVacacionesRhCommand, Result<int>>
    {
        private readonly IMediator _mediator;
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;
        private readonly INomDiasFestivosRepository _festivos;

        public ModificarVacacionesRhCommandHandler(IMediator mediator, INomTramitesRepository tramites,
            INomTramitesFunctions funciones, INomDiasFestivosRepository festivos)
        {
            _mediator = mediator;
            _tramites = tramites;
            _funciones = funciones;
            _festivos = festivos;
        }

        public async Task<Result<int>> Handle(ModificarVacacionesRhCommand request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsAdministradorRh(request.NoEmpActor))
                return await Result<int>.FailureAsync(TramitesRhMensajes.SinPermiso);
            var error = ReglasRh.ValidarMotivo(request.Comentarios, true, "Escriba el motivo de la modificación.");
            if (error != null) return await Result<int>.FailureAsync(error);

            var tramite = await _tramites.GetByIdAsync(request.IdTramite);
            if (tramite == null || tramite.TipoTramite != ReglasRh.TipoVacaciones)
                return await Result<int>.FailureAsync("El trámite no existe o no es de vacaciones.");
            var noEmp = tramite.IdEmpleado;

            var desde = request.Desde.Date;
            var hasta = request.Hasta.Date;
            if (hasta < desde) return await Result<int>.FailureAsync("La fecha final no puede ser anterior a la inicial.");
            if (request.FechaRegreso.HasValue && request.FechaRegreso.Value.Date <= hasta)
                return await Result<int>.FailureAsync("La fecha de regreso debe ser posterior a la fecha final.");
            if (request.IdEmpleadoCubre == noEmp) return await Result<int>.FailureAsync("El empleado no puede cubrirse a sí mismo.");

            var traslape = await _tramites.GetFolioVacacionesTraslapadoAsync(noEmp, desde, hasta, request.IdTramite);
            if (traslape != null) return await Result<int>.FailureAsync($"Las fechas se cruzan con la solicitud {traslape}.");

            var regreso = request.FechaRegreso?.Date
                ?? await CreateSolicitudVacacionesCommandHandler.FechaRegresoAsync(_festivos, noEmp, hasta);

            var resultado = await _tramites.ModificarVacacionesAsync(request.IdTramite, async ct =>
            {
                // Corre dentro de la transacción, ya sin los días de este trámite en NOM_VACAEMP_D.
                var consulta = await _mediator.Send(new GetConsultaVacacionesQuery(noEmp, desde, hasta), ct);
                if (!consulta.Succeeded) return (string.Join(" ", consulta.Messages), null, new List<NomVacaempD>());
                var calculo = consulta.Data;
                if (calculo.DiasHabiles <= 0) return ("El rango no tiene días hábiles.", null, new List<NomVacaempD>());
                if (calculo.Periodos.Sum(p => p.Dias) < calculo.DiasHabiles)
                    return ($"No tiene saldo suficiente para {calculo.DiasHabiles} días.", null, new List<NomVacaempD>());
                if (calculo.Periodos.Any(p => p.Dias != decimal.Truncate(p.Dias) || p.DiasXDisfrutar != decimal.Truncate(p.DiasXDisfrutar)))
                    return ("El reparto por periodo trae medios días; no se puede guardar.", null, new List<NomVacaempD>());

                var datos = new NomVacaempM
                {
                    DiasSolicitados = calculo.DiasHabiles,
                    FDesde = desde,
                    FHasta = hasta,
                    FechaRegreso = regreso,
                    IdEmpleadoCubre = request.IdEmpleadoCubre
                };
                var periodos = calculo.Periodos
                    .Where(p => p.Dias > 0)
                    .Select(p => new NomVacaempD
                    {
                        NoEmp = noEmp,
                        Ano = p.Ano,
                        PerVac = $"{p.Ano}-{p.Periodo}",
                        DiasVac = (int)p.Dias,
                        DiasXDisf = (int)p.DiasXDisfrutar
                    })
                    .ToList();
                return ((string?)null, (NomVacaempM?)datos, periodos);
            }, new NomTramiteBitacora { IdEmpleado = request.NoEmpActor, Accion = "MODIFICADO POR RH", Comentarios = request.Comentarios.Trim() },
            cancellationToken);

            return resultado == null
                ? await Result<int>.SuccessAsync(tramite.Estatus, "Trámite modificado.")
                : await Result<int>.FailureAsync(resultado);
        }
    }

    internal class ModificarPermisoJustificanteRhCommandHandler : IRequestHandler<ModificarPermisoJustificanteRhCommand, Result<int>>
    {
        private readonly INomTramitesRepository _tramites;
        private readonly INomTramitesFunctions _funciones;
        private readonly INomDiasFestivosRepository _festivos;

        public ModificarPermisoJustificanteRhCommandHandler(INomTramitesRepository tramites, INomTramitesFunctions funciones,
            INomDiasFestivosRepository festivos)
        {
            _tramites = tramites;
            _funciones = funciones;
            _festivos = festivos;
        }

        public async Task<Result<int>> Handle(ModificarPermisoJustificanteRhCommand request, CancellationToken cancellationToken)
        {
            if (!await _funciones.EsAdministradorRh(request.NoEmpActor))
                return await Result<int>.FailureAsync(TramitesRhMensajes.SinPermiso);
            var error = ReglasRh.ValidarMotivo(request.Comentarios, true, "Escriba el motivo de la modificación.");
            if (error != null) return await Result<int>.FailureAsync(error);

            var tramite = await _tramites.GetByIdAsync(request.IdTramite);
            if (tramite == null || tramite.TipoTramite is not (ReglasPermisoJustificante.TipoPermisos or ReglasPermisoJustificante.TipoJustificantes))
                return await Result<int>.FailureAsync("El trámite no existe o no es permiso ni justificante.");
            if (!ReglasPermisoJustificante.EsValida(request.TipoIncidencia)
                || ReglasPermisoJustificante.TipoTramite(request.TipoIncidencia) != tramite.TipoTramite)
                return await Result<int>.FailureAsync(tramite.TipoTramite == ReglasPermisoJustificante.TipoPermisos
                    ? "Un permiso solo puede cambiar a otro tipo de permiso."
                    : "Un justificante solo puede cambiar a otro tipo de justificante.");

            var reglas = await ReglasPermisoJustificante.AplicarAsync(_tramites, _festivos, tramite.IdEmpleado, request.TipoIncidencia,
                request.FechaInicio, request.FechaFin, request.JustificacionMotivo, request.IdEmpleadoCubre, request.IdTramite);
            if (reglas.Error != null) return await Result<int>.FailureAsync(reglas.Error);

            var resultado = await _tramites.ModificarPermisoJustificanteAsync(request.IdTramite, d =>
            {
                d.TipoIncidencia = request.TipoIncidencia;
                d.FechaInicio = reglas.Inicio;
                d.FechaFin = reglas.Fin;
                d.JustificacionMotivo = reglas.Motivo;
                // Justificantes no llevan relevo.
                d.IdEmpleadoCubre = tramite.TipoTramite == ReglasPermisoJustificante.TipoPermisos ? request.IdEmpleadoCubre : null;
            }, new NomTramiteBitacora { IdEmpleado = request.NoEmpActor, Accion = "MODIFICADO POR RH", Comentarios = request.Comentarios.Trim() },
            cancellationToken);

            return resultado == null
                ? await Result<int>.SuccessAsync(tramite.Estatus, "Trámite modificado.")
                : await Result<int>.FailureAsync(resultado);
        }
    }
}
