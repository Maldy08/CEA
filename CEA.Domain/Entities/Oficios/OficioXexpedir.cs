using CEA.Domain.Common;


namespace CEA.Domain.Entities.Oficios
{
    public class OficioXexpedir : BaseAuditableEntity
    {
        public int Ejercicio { get; set; }
        public int Depto { get; set; }
        public string NoOficio { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string IntDepen { get; set; } = string.Empty;
        public string IntSiglas { get; set; } = string.Empty;
        public string IntNombre { get; set; } = string.Empty;
        public string IntCargo { get; set; } = string.Empty;
        public string ExtDepen { get; set; } = string.Empty;
        public string ExtSiglas { get; set; } = string.Empty;
        public string ExtNombre { get; set; } = string.Empty;
        public string ExtCargo { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public int Estatus { get; set; }
        public string FechaCaptura { get; set; } = string.Empty;
        public int IdEmpleado { get; set; }
    }
}
