using System;

namespace CEA.Application.DTOs.Checador
{
    // Este DTO representa la información "limpia" que quieres
    public class RegistroAsistenciaDto
    {
        public string IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaPrimera { get; set; }
        public DateTime? FechaUltima { get; set; }
    }
}