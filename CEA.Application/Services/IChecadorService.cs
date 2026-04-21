using CEA.Application.DTOs.Checador;

namespace CEA.Application.Services
{
    public interface IChecadorService
    {
        Task<AcsEventResponse> GetEventosAsync(string deviceKey,AcsEventRequestDto requestBody);
    }
}
