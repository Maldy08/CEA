using CEA.Application.DTOs;
using CEA.Application.Services;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CEA.Application.Features.Email.Commands.SendEmail
{
    public record SendEmailCommand : IRequest<Result<int>>
    {
        public string? To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string? FromName { get; set; }
        public string?[] Cc { get; set; }  = Array.Empty<string>();
        public IFormFile? Attachment { get; set; }

    }
    internal class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result<int>>
    {
        private readonly IEmailService _emailService;

        public SendEmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task<Result<int>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var emailDto = new EmailRequestDto()
            {
                To = request.To,
                Subject = request.Subject,
                Body = request.Body,
                From = request.From,
                FromName = request.FromName,
                Attachment = request.Attachment,
                Cc = request.Cc

            };

            try
            {
                // Enviar el correo 50 veces
               //for (int i = 0; i < 1000; i++)
               //{
                 await _emailService.SendAsync(emailDto);
               //}
                return Result<int>.Success(1);
            }
            catch (Exception)
            {
                return Result<int>.Failure("Error al enviar el correo");
                throw;
            }

        }
    
    }
}
