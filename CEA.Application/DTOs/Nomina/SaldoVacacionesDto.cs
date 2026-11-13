namespace CEA.Application.DTOs.Nomina
{
    // Renglón de F_SALDO_VACACIONES: a) primer periodo con saldo y b) total pendiente hasta hoy.
    public class SaldoVacacionesDto
    {
        public int Empleado { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; }
        public int AntiguedadActualAnos { get; set; }
        // Nulos cuando no tiene ningún periodo con saldo.
        public int? AnoDisponible { get; set; }
        public int? PeriodoDisponible { get; set; }
        // decimal: NOM_VACAPRIMA.DIAS / 2 puede dar medios días.
        public decimal? DiasPorDisfrutar { get; set; }
        public decimal DiasTotalesPorDisfrutar { get; set; }
    }
}
