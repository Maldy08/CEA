using CEA.Application.Common.Mappings;

namespace CEA.Application.DTOs.Transparencia
{
    public class GetFormatoByUserIdDto : IMapFrom<GetFormatoByUserIdDto>
    {
        public int IdDepto { get; set; }
        public List<ReporteDto> Reporte { get; set; } = new List<ReporteDto>();
        //public int IdUsuario { get; set; }  
        //public string Codigo { get; set; } = string.Empty;
        //public string Descripcion { get; set; } = string.Empty;


    }




}
