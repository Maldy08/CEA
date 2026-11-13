namespace CEA.Domain.Entities.Nomina
{
    // Un movimiento de un trámite de RH (NOM_TRAMITES_BITACORA).
    public class NomTramiteBitacora
    {
        public int IdBitacora { get; set; }
        public int IdTramite { get; set; }
        public DateTime FechaMovimiento { get; set; }
        // Quien hizo la acción (sin FK: puede ser un usuario sin ficha, como SISTEMA).
        public int IdEmpleado { get; set; }
        // Estatus en que quedó el trámite con este movimiento.
        public int IdEstatus { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string? Comentarios { get; set; }
    }
}
