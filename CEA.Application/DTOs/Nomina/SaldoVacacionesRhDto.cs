namespace CEA.Application.DTOs.Nomina
{
    // Renglón de la pantalla "Saldos de vacaciones" de RH: empleado activo, su saldo vigente (F_SALDO_VACACIONES)
    // y el último registro de NOM_VACAEMP_M/_D que usa la función como punto de partida.
    public class SaldoVacacionesRhDto
    {
        public int Empleado { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Departamento { get; set; }
        public DateTime FechaAlta { get; set; }
        public int AntiguedadAnos { get; set; }
        // Primer periodo con saldo (nulos si no tiene).
        public int? AnoDisponible { get; set; }
        public int? PeriodoDisponible { get; set; }
        public decimal? DiasPorDisfrutar { get; set; }
        public decimal DiasTotales { get; set; }
        // Último registro (FHASTA más reciente). Nulos = sin punto de partida: F_SALDO cuenta desde el año de alta.
        public string? UltimoOrigen { get; set; }
        public string? UltimoFolio { get; set; }
        public string? UltimoPerVac { get; set; }
        public int? UltimoDiasXDisf { get; set; }
        public DateTime? UltimaFecha { get; set; }
    }

    // Registro de NOM_VACAEMP_M (con su periodo final de NOM_VACAEMP_D) del historial de un empleado.
    public class RegistroVacacionesRhDto
    {
        public int IdVacaemp { get; set; }
        // 'T' trámite del portal / 'R' captura de RH.
        public string Origen { get; set; } = "T";
        public int? IdTramite { get; set; }
        public string? Folio { get; set; }
        public int? Estatus { get; set; }
        public DateTime FDesde { get; set; }
        public DateTime FHasta { get; set; }
        public int DiasSolicitados { get; set; }
        // Último periodo que tocó el registro y lo que le quedó.
        public string? PerVac { get; set; }
        public int? DiasXDisf { get; set; }
        public int? IdEmpleadoCaptura { get; set; }
        public DateTime? FechaCaptura { get; set; }
    }
}
