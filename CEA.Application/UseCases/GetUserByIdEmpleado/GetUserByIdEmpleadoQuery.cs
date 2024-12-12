using AutoMapper;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Application.UseCases.GetUserByIdEmpleado
{
    public record GetUserByIdEmpleadoQuery : IRequest<Result<UserDto>>
    {
        public int IdEmpleado { get; set; }

        public GetUserByIdEmpleadoQuery(int idEmpleado)
        {
            IdEmpleado = idEmpleado;
        }

    }
    internal class GetUserByIdEmpleadoQueryHnadler : IRequestHandler<GetUserByIdEmpleadoQuery, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdEmpleadoQueryHnadler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdEmpleadoQuery request, CancellationToken cancellationToken)
        {
           var usuarios = await _userRepository.GetUserByIdEmpleado(request.IdEmpleado);
            if (usuarios == null)
            {
                return await Task.FromResult(Result<UserDto>.Success("No se encontró el usuario"));
            }
            
            var entitie = _mapper.Map<UserDto>(usuarios);
            return await Task.FromResult(Result<UserDto>.Success(entitie));

        }
    }
}
