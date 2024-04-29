using CEA.Application.DTOs.Transparencia;
using CEA.Domain.Entities.Transparencia;

namespace CEA.Application.Interfaces.Repositories.Transparencia
{
    public interface ITransparenciaFormatoRepository
    {
        Task<List<GetFormatoByUserIdDto>> GetFormatoByUserId(int id);
        Task<GetNombreFormatoDto> GetNombreFormatoByUserId(string nombreFormato);
    }
}
