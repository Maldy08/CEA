namespace CEA.Application.DTOs.IndiArct
{
    // Fila del formulario de captura: una por cada presa del catálogo,
    // con su volumen del año/mes seleccionado (null si aún no se ha capturado).
    public class PresaCapturaDto
    {
        public int IdPresa { get; set; }
        public string NombreOficial { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public int? IdCaptura { get; set; }      // null = aún sin captura para ese año/mes
        public decimal? VolumenM3 { get; set; }  // precarga el input
    }
}
