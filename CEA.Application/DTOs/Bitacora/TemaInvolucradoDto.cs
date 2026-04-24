using AutoMapper;
using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Bitacora;

namespace CEA.Application.DTOs.Bitacora
{
    public class TemaInvolucradoDto : IMapFrom<TemaInvolucrado>
    {
        public int IdAsignacion { get; set; }
        public int IdTema { get; set; }
        public int IdUsuario { get; set; }
        public string TipoInvolucrado { get; set; } = string.Empty;
        public DateTime FechaAsignacion { get; set; }
        public string? NombreUsuario { get; set; }
        public string? TituloTema { get; set; }

        public void Mapping(Profile profile) => profile.CreateMap<TemaInvolucrado, TemaInvolucradoDto>()
            .ForMember(d => d.IdAsignacion, opt => opt.MapFrom(s => s.Id));
    }
}
