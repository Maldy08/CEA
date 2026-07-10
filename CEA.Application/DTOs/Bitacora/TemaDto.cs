using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Bitacora;

namespace CEA.Application.DTOs.Bitacora
{
    public class TemaDto : IMapFrom<Tema>
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaLimite { get; set; }
        public int IdDepartamentoOrigen { get; set; }
        public int? IdCreador { get; set; }
        public string? NombreDepartamento { get; set; }
        public int TotalAvances { get; set; }
        public DateTime? UltimoAvance { get; set; }
    }
}
