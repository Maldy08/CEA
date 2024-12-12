using CEA.Application.DTOs;
using CEA.Application.Features.GetAllUsuarios;
using CEA.Application.Features.GetUserByCredentials;
using CEA.Application.Features.GetUserById;
using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Entities;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CEA.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator, IGenericRepository<User> repository)
        {
            _mediator = mediator;
           
        }

        [HttpGet("GetUserByCredentials")]
        public async Task<ActionResult<Result<UserDto>>> GetUserByCredentials( string login, string password)
        {
            return await _mediator.Send(new GetUserByCredentialsQuery(login,password));
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<Result<UserDto>>> GetUserById(int id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
            
        }

        [HttpGet("GetAllUsuarios")]

        public async Task<ActionResult<Result<List<UserDto>>>> GetAllUsuarios()
        {
            return await _mediator.Send(new GetAllUsuariosQuery());
        }

        [HttpGet("GetUserByIdEmpleado")]
        public async Task<ActionResult<Result<UserDto>>> GetUserByIdEmpleado( int idEmpleado)
        {
            return Ok();
        }

    }
}
