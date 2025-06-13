using CEA.Application.DTOs;
using CEA.Application.Services;
using System.Net;
using System.Net.Mail;


namespace CEA.Infrastructure.Services
{
    public class EmailService : IEmailService
    {

        public async Task SendAsync(EmailRequestDto request)
        {
            var emailClient = new SmtpClient("172.31.74.245", 25);
            emailClient.Credentials = new NetworkCredential("sisco@ceabc.gob.mx", "c0s43mxl");
            emailClient.EnableSsl = false;
            var message = new MailMessage
            {
                From = new MailAddress(request.From, "Sistema de Control de Oficios"),
                Subject = request.Subject,
                IsBodyHtml = true,
                Body = request.Body,


            };
            request.To = request.To ?? string.Empty;
            if(!string.IsNullOrWhiteSpace(request.To))
            {
                message.To.Add(request.To);
            }
            if (request.Cc != null)
            {
                foreach (var cc in request.Cc)
                {
                    message.CC.Add(cc!);
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
