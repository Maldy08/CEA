using AutoMapper;
using CEA.Application.DTOs.Viaticos;

namespace CEA.Application.Common.Mappings
{
    public class EmpleadoMapping : Profile
    {
        public EmpleadoMapping()
        {
            CreateMap<Domain.Entities.RecursosHumanos.Empleado, EmpleadoDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdEmpleado));
        }
    }
}
