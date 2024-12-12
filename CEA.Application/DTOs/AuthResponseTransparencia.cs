
using CEA.Application.DTOs.Transparencia;
using System.Text.Json.Serialization;

namespace CEA.Application.DTOs
{
    public class AuthResponseTransparencia
    {
        public string? Message { get; set; } 
        public bool IsAuthenticated { get; set; }
        
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public int Id { get; set; }
        public string? Token { get; set; }
        public string? Name { get; set; }
        public string User { get; set; } = null!;

        public DateTime? TokenExpiresOn { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }

        public UserDtoTransparencia UserData { get; set; } = new UserDtoTransparencia();
    }
}
