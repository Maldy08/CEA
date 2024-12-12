
using AutoMapper;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.GetUserByCredentials
{

    public record GetUserByCredentialsQuery : IRequest<Result<UserDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public GetUserByCredentialsQuery(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
    internal class GetUserByCredentialsQueryHandler : IRequestHandler<GetUserByCredentialsQuery, Result<UserDto>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByCredentialsQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken)
        {
           
            var user = await _userRepository.GetUserByCredentials(request.Username, request.Password);
            var userDto = _mapper.Map<UserDto>(user);
            return await Result<UserDto>.SuccessAsync(userDto);
        }
    }
}
