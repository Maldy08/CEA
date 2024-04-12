using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Application.Common.Mappings
{
    public class EmpleadoMapping : Profile
    {
        public EmpleadoMapping()
        {
            CreateMap<Domain.Entities.RecursosHumanos.Empleado, Application.DTOs.EmpleadoDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdEmpleado));
        }
    }
}
