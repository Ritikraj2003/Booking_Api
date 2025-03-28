using MailKit.Net.Smtp;
using MimeKit;

namespace Booking_Api.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com"; // Update SMTP Server
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "ritikbaryons@gmail.com"; // Your Email
        private readonly string _smtpPass = "qpipmrebvbylskvs"; // Your Email Password

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // ✅ Validate Email Address
            if (string.IsNullOrWhiteSpace(toEmail))
            {
                throw new ArgumentNullException(nameof(toEmail), "Receiver email address cannot be null or empty.");
            }

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Booking API", _smtpUser));
            email.To.Add(new MailboxAddress(toEmail, toEmail)); // This line fails if `toEmail` is null
            email.Subject = subject;
            email.Body = new TextPart("plain") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_smtpUser, _smtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
