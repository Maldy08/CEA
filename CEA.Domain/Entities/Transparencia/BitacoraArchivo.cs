

using CEA.Domain.Common;

namespace CEA.Domain.Entities.Transparencia
{
    public class BitacoraArchivo: BaseAuditableEntity
    {
        public int IdBitacora { get; set; }

        public string NombreReporte { get; set; } = null!;

        public string NombreArchivo { get; set; } = null!;

        public string? Hipervinculo { get; set; }

        public string? RutaArchivo { get; set; }

        public DateTime? FechaSubido { get; set; }

        public DateTime? FechaModificado { get; set; }

        public int IdUsuario { get; set; }

        public int? Periodo { get; set; }
    }
}
