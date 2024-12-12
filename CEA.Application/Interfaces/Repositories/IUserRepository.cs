

using CEA.Application.DTOs;

namespace CEA.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserByCredentials(string username, string password);
        Task<UserDto> GetUserById(int id);
        Task<List<UserDto>> GetAllUsuarios();
        Task<UserDto> GetUserByIdEmpleado(int idEmpleado);
       // Task<UserDto> GetUserByEmail(string email);

    }
}
