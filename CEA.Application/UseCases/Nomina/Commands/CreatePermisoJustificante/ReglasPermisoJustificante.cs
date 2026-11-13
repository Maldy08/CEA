using CEA.Application.Interfaces.Repositories.Nomina;

namespace CEA.Application.UseCases.Nomina.Commands.CreatePermisoJustificante
{
    // Reglas de un permiso (incidencia 1-2 → TIPO_TRAMITE 3) o justificante (3-7 → TIPO_TRAMITE 4). Las usan la
    // generación (CreatePermisoJustificante) y la modificación de RH (ModificarPermisoJustificanteRh).
    internal static class ReglasPermisoJustificante
    {
        public const int TipoPermisos = 3;
        public const int TipoJustificantes = 4;

        public static bool EsValida(int tipoIncidencia) => tipoIncidencia is >= 1 and <= 7;

        public static int TipoTramite(int tipoIncidencia) => tipoIncidencia <= 2 ? TipoPermisos : TipoJustificantes;

        public record Resultado(string? Error, DateTime Inicio, DateTime Fin, int? DiasHabiles, string Motivo);

        // Valida y normaliza fechas y motivo, y revisa cruces con otros trámites vigentes del empleado.
        // `excluirIdTramite`: el trámite que se modifica.
        public static async Task<Resultado> AplicarAsync(INomTramitesRepository tramites, INomDiasFestivosRepository festivos,
            int noEmp, int tipoIncidencia, DateTime fechaInicio, DateTime fechaFin, string? justificacionMotivo,
            int? idEmpleadoCubre, int? excluirIdTramite = null)
        {
            Resultado Fallo(string mensaje) => new(mensaje, fechaInicio, fechaFin, null, "");

            if (!EsValida(tipoIncidencia)) return Fallo("Tipo de incidencia no válido.");
            var esPermiso = tipoIncidencia <= 2;

            // Permisos y no checada (7): días completos. Justificación de entrada/salida (3-4): un día, hora inicio < fin.
            // No checada de entrada/salida (5-6): un día y una hora (fin = inicio).
            var inicio = fechaInicio;
            var fin = fechaFin;
            switch (tipoIncidencia)
            {
                case 1 or 2 or 7:
                    inicio = inicio.Date;
                    fin = fin.Date;
                    break;
                case 3 or 4:
                    if (fin.Date != inicio.Date) return Fallo("La justificación de entrada o salida es de un solo día.");
                    if (fin <= inicio) return Fallo("La hora final debe ser posterior a la inicial.");
                    break;
                default:
                    fin = inicio;
                    break;
            }
            if (fin < inicio) return Fallo("La fecha final no puede ser anterior a la inicial.");
            if (idEmpleadoCubre == noEmp) return Fallo("El empleado no puede cubrirse a sí mismo.");
            var motivo = justificacionMotivo?.Trim() ?? "";
            if (motivo.Length == 0) return Fallo("Escriba la justificación.");
            if (motivo.Length > 500) return Fallo("La justificación no puede pasar de 500 caracteres.");

            int? diasHabiles = null;
            if (esPermiso)
            {
                diasHabiles = await DiasHabilesAsync(festivos, noEmp, inicio, fin);
                if (diasHabiles == 0) return Fallo("El rango no tiene días hábiles (solo fines de semana o días festivos).");
                var vacaciones = await tramites.GetFolioVacacionesTraslapadoAsync(noEmp, inicio, fin);
                if (vacaciones != null) return Fallo($"Las fechas se cruzan con la solicitud de vacaciones {vacaciones}.");
            }
            // Por día completo: dos permisos no pueden caer en el mismo día; en justificantes, solo si es la misma
            // incidencia (no checada de entrada y de salida del mismo día sí van juntas).
            var traslape = await tramites.GetFolioPermisoTraslapadoAsync(noEmp, TipoTramite(tipoIncidencia),
                esPermiso ? null : tipoIncidencia, inicio.Date, fin.Date.AddDays(1).AddSeconds(-1), excluirIdTramite);
            if (traslape != null) return Fallo($"Las fechas se cruzan con el trámite {traslape}.");

            return new Resultado(null, inicio, fin, diasHabiles, motivo);
        }

        // Lunes a viernes del rango sin los festivos de día completo del empleado (misma regla que FN_DIAS_HABILES_EMP).
        private static async Task<int> DiasHabilesAsync(INomDiasFestivosRepository festivos, int noEmp, DateTime desde, DateTime hasta)
        {
            var inhabiles = (await festivos.GetByEmpleadoAsync(noEmp, desde, hasta))
                .Where(f => f.HoraEntrada == null && f.HoraSalida == null)
                .Select(f => f.FechaFestivo.Date)
                .ToHashSet();
            var dias = 0;
            for (var dia = desde; dia <= hasta; dia = dia.AddDays(1))
                if (dia.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday) && !inhabiles.Contains(dia))
                    dias++;
            return dias;
        }
    }
}
