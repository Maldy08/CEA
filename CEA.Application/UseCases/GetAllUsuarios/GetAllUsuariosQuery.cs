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

namespace CEA.Application.Features.GetAllUsuarios
{
    public record GetAllUsuariosQuery : IRequest<Result<List<UserDto>>>
    {
    }
    internal class GetAllUsuariosQueryHandler : IRequestHandler<GetAllUsuariosQuery, Result<List<UserDto>>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsuariosQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<UserDto>>> Handle(GetAllUsuariosQuery request, CancellationToken cancellationToken)
        { 
            var usuarios = await _userRepository.GetAllUsuarios();
            var usuariosDto = _mapper.Map<List<UserDto>>(usuarios);
            return await Result<List<UserDto>>.SuccessAsync(usuariosDto);
        }
    }

}
