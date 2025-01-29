namespace CEA.Application.DTOs.Oficios
{
    public class OficioListaDepartamentosDto
    {
        public int IdCea { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? Siglas { get; set; } = string.Empty;
        public string Responsable { get; set; } = string.Empty;
        public string Puesto { get; set; } = string.Empty;
    }
}
