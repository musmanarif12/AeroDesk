using System.Net;
using System.Net.Mail;
using AeroDesk.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AeroDesk.Infrastructure.Email
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(
            string toEmail,
            string subject,
            string htmlBody,
            CancellationToken cancellationToken)
        {
            var host = _configuration["Smtp:Host"]!;
            var port = int.Parse(_configuration["Smtp:Port"]!);
            var username = _configuration["Smtp:Username"]!;
            var password = _configuration["Smtp:Password"]!;
            var fromEmail = _configuration["Smtp:FromEmail"]!;
            var fromName = _configuration["Smtp:FromName"]!;

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            using var message = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            await client.SendMailAsync(message, cancellationToken);
        }
    }
}