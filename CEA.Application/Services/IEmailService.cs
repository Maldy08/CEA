

using CEA.Application.DTOs;

namespace CEA.Application.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto emailRequestDto);
    }
}
