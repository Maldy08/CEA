

using CEA.Application.DTOs.Transparencia;

namespace CEA.Application.Interfaces.Repositories.Transparencia
{
    public interface IUserTransparenciaRepository
    {
        Task<UserDtoTransparencia> GetUserByCredentials(string username, string password);
        Task<UserDtoTransparencia> GetUserById(int id);
    }
}
