using AutoMapper;
using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Bitacora;

namespace CEA.Application.DTOs.Bitacora
{
    public class AdjuntoDto
    {
        public int IdAdjunto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? TipoMime { get; set; }
    }

    public class AvanceDto : IMapFrom<Avance>
    {
        public int IdAvance { get; set; }
        public int IdTema { get; set; }
        public string TituloTema { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public List<AdjuntoDto> Adjuntos { get; set; } = new();

        public void Mapping(Profile profile) => profile.CreateMap<Avance, AvanceDto>()
            .ForMember(d => d.IdAvance, opt => opt.MapFrom(s => s.Id));
    }
}
