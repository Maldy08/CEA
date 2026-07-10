using CEA.Application.DTOs;
using CEA.Application.Services;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Net.Mail;


namespace CEA.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendAsync(EmailRequestDto request)
        {
            var emailClient = new SmtpClient("172.31.74.245", 25);
            emailClient.Credentials = new NetworkCredential("sisco@ceabc.gob.mx", "c0s43mxl");
            emailClient.EnableSsl = false;
            var message = new MailMessage
            {
                From = new MailAddress(request.From, string.IsNullOrWhiteSpace(request.FromName) ? "CEABC" : request.FromName),
                Subject = request.Subject,
                IsBodyHtml = true,
                Body = request.Body,


            };
            request.To = request.To ?? string.Empty;
            if(!string.IsNullOrWhiteSpace(request.To))
            {
                try
                {
                    message.To.Add(request.To.Trim());
                }
                catch (FormatException)
                {
                    _logger.LogWarning("Correo destinatario (To) con formato inválido, se omite: {To}", request.To);
                }
            }
            if (request.Cc != null)
            {
                const string correoSoporte = "soporte@ceabc.gob.mx";
                foreach (var cc in request.Cc)
                {
                    if (string.IsNullOrWhiteSpace(cc))
                    {
                        // El destinatario no tiene correo: se envía la copia a soporte en su lugar.
                        if (!message.CC.Any(a => string.Equals(a.Address, correoSoporte, StringComparison.OrdinalIgnoreCase)))
                        {
                            message.CC.Add(correoSoporte);
                        }
                        continue;
                    }
                    try
                    {
                        message.CC.Add(cc.Trim());
                    }
                    catch (FormatException)
                    {
                        _logger.LogWarning("Correo en copia (Cc) con formato inválido, se omite: {Cc}", cc);
                    }
                }
            }
            if (request.Attachment != null)
            {
                var ms = new MemoryStream();
                await request.Attachment.CopyToAsync(ms);
                ms.Position = 0;
                message.Attachments.Add(new Attachment(ms, request.Attachment.FileName));
            }

            await emailClient.SendMailAsync(message);

        }
    }
}
