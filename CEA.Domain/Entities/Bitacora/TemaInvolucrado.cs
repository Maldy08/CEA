using CEA.Domain.Common;

namespace CEA.Domain.Entities.Bitacora
{
    public class TemaInvolucrado : BaseAuditableEntity
    {
        public int IdTema { get; set; }
        public int IdUsuario { get; set; }
        public string TipoInvolucrado { get; set; } = "Visualizador";
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;
    }
}
