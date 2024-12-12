using CEA.Application.DTOs;
using System.Security.Claims;

namespace CEA.Application.Services
{
    public interface IAuthServiceTransparencia
    {
        Task<AuthResponseTransparencia> LoginAsync(string user, string password);
        ClaimsPrincipal DecodeToken(string token);
        AuthResponseTransparencia ValidateToken(string token);
    }
}
