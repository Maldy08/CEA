

using Microsoft.AspNetCore.Http;

namespace CEA.Application.DTOs.Transparencia
{
    public class BitacoraArchivoDto
    {
        public int idBitacora { get; set; }
        public string codigo { get; set; } = string.Empty;
        public int idUsuario { get; set; }
        public int trimestre { get; set; }
        public int periodo { get; set; }
        public List<IFormFile> archivos { get; set; } = new List<IFormFile>();
    }
}
