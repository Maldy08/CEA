

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Oficios;
using System.ComponentModel.DataAnnotations.Schema;

namespace CEA.Application.DTOs.Oficios
{
    public class OficioDtoFunction : IMapFrom<OficioDtoFunction>
    {

        public string Ren {  get; set; } = string.Empty;
        public int Id { get; set; }
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string NoOficio { get; set; } = null!;
        public string? Pdfpath { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } //Fecha del documento
        public DateTime FechaCaptura { get; set; } = DateTime.Now;
        public DateTime? FechaAcuse { get; set; } //Opcional, siempre y cuando el oficio pida una fecha de respuesta y unicamente aplica a eor = 1
        public DateTime? FechaLimite { get; set; }  //Opcional, siempre y cuando el oficio pida uan fecha limite
        public string RemDepen { get; set; } = string.Empty;
        public string RemSiglas { get; set; } = string.Empty;
        public string RemNombre { get; set; } = string.Empty;
        public string RemCargo { get; set; } = string.Empty;
        public string DestDepen { get; set; } = string.Empty;
        public string DestSiglas { get; set; } = string.Empty;
        public string DestNombre { get; set; } = string.Empty;
        public string DestCargo { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public string? Estatus { get; set; } = string.Empty;
        public int? Empqentrega { get; set; }
        public string? Relacionoficio { get; set; }
        public int Depto { get; set; }
        public int DeptoRespon { get; set; }
        public string? Observaciones { get; set; } = string.Empty;
        public int Rol { get; set; }

        public int EstatusNum { get; set; } //Estatus numerico


        [NotMapped]
        public List<OficioBitacoraDto> OficioBitacora { get; set; } = new();
        [NotMapped]
        public List<OficioResponsableDto> OficiosResponsables { get; set; } = new();

        //Necesita una dependencia de OficiosBitacora
        //Se relaciona con ejercicio,folio,eor,fechacaptura
        //public OficioBitacora OficioBitacora { get; set; } = new();
        //public List<OficioResponsable> OficiosResponsables { get; set; } = new();
    }
}
