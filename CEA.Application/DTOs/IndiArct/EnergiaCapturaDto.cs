namespace CEA.Application.DTOs.IndiArct
{
    // Fila del formulario de captura de energía: una por cada mes (1..12)
    // del año seleccionado, con sus valores (null si aún no se ha capturado).
    public class EnergiaCapturaDto
    {
        public int Mes { get; set; }
        public int? IdEnergia { get; set; }   // null = aún sin captura para ese año/mes
        public decimal? Volumenes { get; set; }
        public decimal? Kwh { get; set; }
        public decimal? Costo { get; set; }
    }
}
