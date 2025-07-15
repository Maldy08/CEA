namespace CEA.Application.DTOs.Oficios
{
    public class OficioListaDashboardDto
    {
        public string Letra { get; set; } = string.Empty;
        public int Eor { get; set; }
        public int Folio { get; set; }
        public string Fecha { get; set; } = string.Empty;
        public int Tipo { get; set; }
        public string NomTipo { get; set; } = string.Empty;
        public string NoOficio { get; set; } = string.Empty;
        public int Rn { get; set; }
        public int Rol { get; set; }
    }
}
