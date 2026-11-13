namespace CEA.Application.DTOs.Nomina
{
    // Resultado de guardar un permiso o justificante (NOM_TRAMITES + NOM_PERMISOS_JUSTIFICANTES).
    public class PermisoJustificanteCreadoDto
    {
        public int IdTramite { get; set; }
        public int IdPermisoJustificante { get; set; }
        // PER-NNNN-AAAA o JUS-NNNN-AAAA.
        public string Folio { get; set; } = string.Empty;
        // Días hábiles del permiso; nulo en justificantes.
        public int? DiasHabiles { get; set; }
    }
}
