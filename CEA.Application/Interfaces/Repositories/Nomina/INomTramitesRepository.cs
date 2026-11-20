using CEA.Application.DTOs.Nomina;
using CEA.Domain.Entities.Nomina;

namespace CEA.Application.Interfaces.Repositories.Nomina
{
    // Trámites de RH (NOM_TRAMITES + NOM_TRAMITES_BITACORA) y su detalle de vacaciones (NOM_VACAEMP_M/_D).
    public interface INomTramitesRepository
    {
        // Trámites del empleado (beneficiario) en el año; tipoTramite nulo = todos los tipos.
        Task<List<NomTramiteDto>> GetByEmpleadoAsync(int noEmp, int ano, int? tipoTramite);
        // Trámite con su bitácora; nulo si no existe.
        Task<NomTramiteDto?> GetByIdAsync(int idTramite);
        // Solicitud de vacaciones del trámite con sus periodos; nula si el trámite no tiene.
        Task<SolicitudVacacionesDto?> GetSolicitudVacacionesAsync(int idTramite);
        // Folio de una solicitud vigente (no cancelada) del empleado que se cruce con el rango; nulo si no hay.
        // `excluirIdTramite`: el trámite que se está modificando (no choca consigo mismo).
        Task<string?> GetFolioVacacionesTraslapadoAsync(int noEmp, DateTime desde, DateTime hasta, int? excluirIdTramite = null);
        // Guarda en UNA transacción: trámite (con folio <SIGLAS>-NNNN-AAAA consecutivo por tipo y año),
        // su primer movimiento de bitácora, la solicitud M y sus periodos D. Regresa trámite y M con sus IDs y folio.
        // Pasa el trámite a `nuevo` solo si su estatus actual está en `permitidos` (renglón bloqueado con FOR UPDATE),
        // con su movimiento de bitácora. Con estatus 99 llena FECHA_CANCELACION y, si `borrarDetalleVacaciones`,
        // borra NOM_VACAEMP_D (pestaña Procesos). Regresa el estatus que tenía; nulo si el trámite no existe.
        Task<int?> CambiarEstatusAsync(int idTramite, IReadOnlyCollection<int> permitidos, int nuevo,
            NomTramiteBitacora bitacora, bool borrarDetalleVacaciones, CancellationToken cancellationToken);
        Task<(NomTramite Tramite, NomVacaempM Solicitud)> CreateSolicitudVacacionesAsync(
            NomTramite tramite, NomTramiteBitacora bitacora, NomVacaempM solicitud, List<NomVacaempD> periodos,
            CancellationToken cancellationToken);
        // Folio de un permiso/justificante vigente (no cancelado) del empleado, del mismo tipo de trámite (y de la
        // misma incidencia si `tipoIncidencia` no es nulo), que se cruce con el rango; nulo si no hay.
        Task<string?> GetFolioPermisoTraslapadoAsync(int noEmp, int tipoTramite, int? tipoIncidencia, DateTime desde, DateTime hasta,
            int? excluirIdTramite = null);
        // Guarda en UNA transacción el trámite (folio PER-/JUS-NNNN-AAAA), su primer movimiento y su renglón de
        // NOM_PERMISOS_JUSTIFICANTES.
        Task<(NomTramite Tramite, NomPermisoJustificante Detalle)> CreatePermisoJustificanteAsync(
            NomTramite tramite, NomTramiteBitacora bitacora, NomPermisoJustificante detalle, CancellationToken cancellationToken);

        // ---- Administración de Trámites de RH ----

        // Todos los trámites del año (sin viáticos), con nombre del empleado y fechas del detalle. Filtros opcionales.
        Task<List<TramiteRhDto>> GetTodosAsync(int ano, int? tipoTramite, int? estatus);
        // Bandeja de firmas: trámites (sin viáticos, de cualquier año) que esperan la firma del empleado:
        // estatus 2 con VALIDA_1 = él (jefe) o estatus 3 con VALIDA_2 = él (director).
        Task<List<TramiteRhDto>> GetPorFirmarAsync(int noEmp);
        // Detalle de permiso/justificante del trámite; nulo si no tiene.
        Task<PermisoJustificanteDto?> GetPermisoJustificanteAsync(int idTramite);
        // Rehace la solicitud de vacaciones en UNA transacción (trámite bloqueado con FOR UPDATE): borra su NOM_VACAEMP_D
        // para que el saldo no cuente sus propios días, llama a `recalcular` (corre en la misma transacción, así
        // FN_CONSULTA_VACACIONES ya no ve esos días), actualiza NOM_VACAEMP_M, inserta los periodos nuevos y la
        // bitácora (con el estatus que tenga el trámite). Si `recalcular` regresa error, deshace todo.
        // Regresa el mensaje de error o nulo si se guardó.
        Task<string?> ModificarVacacionesAsync(int idTramite,
            Func<CancellationToken, Task<(string? Error, NomVacaempM? Datos, List<NomVacaempD> Periodos)>> recalcular,
            NomTramiteBitacora bitacora, CancellationToken cancellationToken);
        // Aplica `aplicar` al renglón de NOM_PERMISOS_JUSTIFICANTES y agrega la bitácora, en una transacción con el
        // trámite bloqueado. Regresa el mensaje de error o nulo si se guardó.
        Task<string?> ModificarPermisoJustificanteAsync(int idTramite, Action<NomPermisoJustificante> aplicar,
            NomTramiteBitacora bitacora, CancellationToken cancellationToken);

        // ---- Saldos de vacaciones capturados por RH (NOM_VACAEMP_M.ORIGEN_REGISTRO = 'R') ----

        // Empleados activos con su saldo (F_SALDO_VACACIONES) y su último registro M/D.
        Task<List<SaldoVacacionesRhDto>> GetSaldosVacacionesAsync();
        // Historial M/D del empleado, más reciente primero.
        Task<List<RegistroVacacionesRhDto>> GetRegistrosVacacionesAsync(int noEmp);
        // Días del tabulador (NOM_VACAPRIMA.DIAS / 2) para un periodo con `antiguedad` años; nulo si no hay renglón.
        Task<decimal?> DiasPorPeriodoAsync(int antiguedad);
        // Folio del trámite de vacaciones del empleado con días en NOM_VACAEMP_D y FHASTA >= `fecha`; nulo si no hay.
        Task<string?> GetFolioVacacionesPosteriorAsync(int noEmp, DateTime fecha);
        // Inserta M (origen R) + su periodo D en una transacción; regresa ID_VACAEMP.
        Task<int> CreateRegistroRhAsync(NomVacaempM registro, NomVacaempD periodo, CancellationToken cancellationToken);
        // Borra un registro de origen R (D y M). Regresa false si no existe o no es de RH.
        Task<bool> DeleteRegistroRhAsync(int idVacaemp, CancellationToken cancellationToken);
    }
}
