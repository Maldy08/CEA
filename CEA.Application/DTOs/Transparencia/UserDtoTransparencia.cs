

using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Application.DTOs.Transparencia
{
    public class UserDtoTransparencia : IMapFrom<Usuario>
    {
        public int IdUsuario { get; set; }

        public string Usuario { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int IdNivel { get; set; }

        public int Activo { get; set; }

        public int IdDepto { get; set; }

        public int IdPuesto { get; set; }
    }
}
