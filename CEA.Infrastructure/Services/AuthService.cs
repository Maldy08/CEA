
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Services;
using CEA.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CEA.Infrastructure.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly JWT _jwt;

        public AuthService(IUserRepository userRepository, IEmpleadoRepository empleadoRepository, IOptions<JWT> jwt)
        {
            _userRepository = userRepository;
            _empleadoRepository = empleadoRepository;
            _jwt = jwt.Value;
        }

        public async Task<AuthResponse> LoginAsync(string user, string password)
        {

            var credential = await _userRepository.GetUserByCredentials(user, password);

            if (credential == null)
            {
                return null;
            }

            // var token = GenerateToken(credential);
            var empleado = await _empleadoRepository.GetEmpleadoByIdAsync(credential.NoEmpleado);

            return new AuthResponse
            {
                Token = CreateJwt(credential),
                Email = empleado.Correo,
                IsAuthenticated = true,
                Message = "User Authenticated",
                Roles = empleado.DeptoUe == 23 ? new List<string> { "Admin" } : new List<string> { "User" },
                TokenExpiresOn = DateTime.Now.AddHours(1),
                Username = credential.Login,
                UserData = new UserDto
                {
                    Login = credential.Login,
                    Pass = credential.Pass,
                    Activo = credential.Activo,
                    Depto = credential.Depto,
                    DeptoDescripcion = credential.DeptoDescripcion,
                    Descripcion = credential.Descripcion,
                    IdPue = credential.IdPue,
                    NoEmpleado = credential.NoEmpleado,
                    NombreCompleto = credential.NombreCompleto,
                    Usuario = credential.Usuario,
                    Municipio = credential.Municipio,
                    Oficina = credential.Oficina,
                    Activos = credential.Activos,
                    ActivosNivel = credential.ActivosNivel,
                    Almacen = credential.Almacen,
                    AlmacenNivel = credential.AlmacenNivel,
                    Bd = credential.Bd ?? 0,
                    Caja = credential.Caja,
                    CajaNivel = credential.CajaNivel,
                    Compras = credential.Compras,
                    ComprasNivel = credential.ComprasNivel,
                    Contabilidad = credential.Contabilidad,
                    ContabilidadNivel = credential.ContabilidadNivel,
                    Nominas = credential.Nominas,
                    NominasNivel = credential.NominasNivel,
                    Polnom = credential.Polnom,
                    Presupuestos = credential.Presupuestos,
                    PresupuestosNivel = credential.PresupuestosNivel,
                    Vales = credential.Vales,
                    ValesNivel = credential.ValesNivel,
                    Viaticos = credential.Viaticos,
                    ViaticosNivel = credential.ViaticosNivel,
                    Oficios = credential.Oficios,
                    OficiosNivel = credential.OficiosNivel

                }
            };
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
                    ClockSkew = System.TimeSpan.Zero
                }, out var validatedToken);

            return tokenHandler;
        }

        private string CreateJwt(UserDto credential)
        {
            var roleClaims = new List<Claim>();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, credential.Login),
                new Claim("userid", credential.NoEmpleado.ToString()),
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

        public AuthResponse ValidateToken(string token)
        {
            var handler = DecodeToken(token);
            if (handler!.Claims.Any())
            {
                var user = handler.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var id = int.Parse(handler.Claims.FirstOrDefault(x => x.Type == "userid")?.Value);
                return new AuthResponse
                {
                    Token = token,
                    IsAuthenticated = true,
                    Message = "User Authenticated",
                    Roles = new List<string> { "User" },
                    TokenExpiresOn = DateTime.Now.AddHours(1),
                    Username = user
                };
            }
            else
            {
               return new AuthResponse
                {
                    Token = null,
                    IsAuthenticated = false,
                    Message = "User not Authenticated",
                    Roles = new List<string> { "User" },
                  //  TokenExpiresOn = DateTime.Now.AddHours(1),
                    Username = ""
                };
            }
        }

        public Task<AuthResponse> ValidateUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
