
using CEA.Application.DTOs;
using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Application.Services;
using CEA.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CEA.Infrastructure.Services
{
    public class AuthServiceTransparencia : IAuthServiceTransparencia
    {
        private readonly IUserTransparenciaRepository _userTransparenciaRepository;
        private readonly JWT _jwt;

        public AuthServiceTransparencia(IUserTransparenciaRepository userTransparenciaRepository, IOptions<JWT> jwt)
        {
            _userTransparenciaRepository = userTransparenciaRepository;
            _jwt = jwt.Value;
        }

        public ClaimsPrincipal DecodeToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var tokenHandler = new JwtSecurityTokenHandler()
                .ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwt.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

            return tokenHandler;
        }

        public async Task<AuthResponseTransparencia> LoginAsync(string user, string password)
        {
            var credential = await _userTransparenciaRepository.GetUserByCredentials(user, password);
            if (credential == null) {
                return null!;
            }

            return new AuthResponseTransparencia
            {
                Id = credential.IdUsuario,
                Token = CreateJwt(credential),
                Name = credential.Usuario,
                User = credential.Usuario,
                IsAuthenticated = true,
                Message = "User Authenticated",
                TokenExpiresOn = DateTime.Now.AddHours(1),
                
                UserData = new UserDtoTransparencia
                {
                    Activo = credential.Activo,
                    Descripcion = credential.Descripcion,
                   IdDepto = credential.IdDepto,
                   IdNivel = credential.IdNivel, 
                   IdPuesto = credential.IdPuesto,
                   IdUsuario = credential.IdUsuario,
                   Usuario = credential.Usuario
                }
            };
        }

        public AuthResponseTransparencia ValidateToken(string token)
        {
            var handler = DecodeToken(token);
            if (handler!.Claims.Any())
            {
                var user = handler.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var id = int.Parse(handler.Claims.FirstOrDefault(x => x.Type == "userid")?.Value!);
                return new AuthResponseTransparencia
                {
                    Id = id,
                    Token = token,
                    Name = user,
                    User = user!,
                    IsAuthenticated = true,
                    Message = "User Authenticated",
                    Roles = new List<string> { "User" },
                    TokenExpiresOn = DateTime.Now.AddHours(1),
                    
                };
            }
            else
            {
                return new AuthResponseTransparencia
                {
                    Token = null,
                    IsAuthenticated = false,
                    Message = "User not Authenticated",
                    Roles = new List<string> { "User" },
                    //  TokenExpiresOn = DateTime.Now.AddHours(1),
                    User = ""
                };
            }
        }
    

        private string CreateJwt(UserDtoTransparencia credential)
        {
            var roleClaims = new List<Claim>();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, credential.Descripcion!),
                new Claim("userid", credential.IdUsuario.ToString()),
               // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               // new Claim(JwtRegisteredClaimNames.Email,""),
               // new Claim("NoEmpleado", credential.NoEmpleado.ToString())
             };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                    signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
