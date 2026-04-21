using CEA.Domain.Common;

namespace CEA.Domain.Entities.Checador
{
    public class RegistroChecador : BaseAuditableEntity
    {
        public int IdEmpleado { get; set; }
        public DateTime FechaHora { get; set; }
        public string NombreEmpleado { get; set; }
        public int TipoEventoMayor { get; set; } // 'major' del JSON
        public int TipoEventoMenor { get; set; } // 'minor' del JSON
        public int DoorNo { get; set; }
        public string PictureURL { get; set; }
        public int SerialNo { get; set; }
    }
}
