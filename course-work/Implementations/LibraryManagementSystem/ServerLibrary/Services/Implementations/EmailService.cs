using Microsoft.Extensions.Configuration;
using MimeKit;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

using System.Threading.Tasks;

namespace Server.Services.Implementations
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpHost = _configuration["Email:SmtpHost"];
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"]);
            var smtpUser = _configuration["Email:SmtpUser"];
            var smtpPass = _configuration["Email:SmtpPass"];
            var fromEmail = _configuration["Email:FromEmail"];
            var fromName = _configuration["Email:FromName"];

            var message = new MailMessage();
            message.From = new MailAddress(fromEmail, fromName);
            message.To.Add(new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }

    }
}
