using CEA.Application.DTOs.Transparencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Application.Interfaces.Repositories.Transparencia
{
    public interface IFormatoRepository
    {
        Task<List<FormatoDto>> GetFormatos();
        Task<List<FormatoDto>> GetFormatosByIdUser(int userId);
    }
}
