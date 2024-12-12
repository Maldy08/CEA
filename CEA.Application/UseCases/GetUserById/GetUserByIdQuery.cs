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

namespace CEA.Application.Features.GetUserById
{
    public record GetUserByIdQuery : IRequest<Result<UserDto>>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

    }

    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            var userDto = _mapper.Map<UserDto>(user);
            return await Result<UserDto>.SuccessAsync(userDto);
        }
    }
}
