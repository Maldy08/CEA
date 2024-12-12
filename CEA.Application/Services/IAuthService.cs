
using CEA.Application.DTOs;
using System.Security.Claims;

namespace CEA.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(string user, string password);
        ClaimsPrincipal DecodeToken(string token);
        AuthResponse ValidateToken(string token);
        Task<AuthResponse> ValidateUserByEmail(string email);

    }
}
