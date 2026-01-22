using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PuppyStore.Shared.Models;

namespace PuppyStore.Server.Services
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = "";
        public int SmtpPort { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendContactAsync(ContactForm form)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_settings.Username, "Puppy Love Website"),
                Subject = $"New contact message from {form.Name}",
                Body = $"Name: {form.Name}\nEmail: {form.Email}\n\nMessage:\n{form.Message}",
                IsBodyHtml = false
            };

            mail.To.Add(_settings.Username);

            using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_settings.Username, _settings.Password)
            };

            await client.SendMailAsync(mail);
        }
        public async Task SendOrderConfirmationAsync(string toEmail, string body)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_settings.Username, "Puppy Love Store"),
                Subject = "Your Puppy Love Order Confirmation 🐾",
                Body = body,
                IsBodyHtml = false
            };

            mail.To.Add(toEmail);

            using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_settings.Username, _settings.Password)
            };

            await client.SendMailAsync(mail);
        }

    }
}
