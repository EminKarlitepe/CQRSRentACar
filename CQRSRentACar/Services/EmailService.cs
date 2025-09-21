using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CQRSRentACar.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string toName, string subject, string message, string aiResponse)
        {
            try
            {
                var smtpHost = _configuration["EmailSettings:SmtpHost"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var smtpUsername = _configuration["EmailSettings:SmtpUsername"];
                var smtpPassword = _configuration["EmailSettings:SmtpPassword"].Replace(" ", "");
                var fromEmail = _configuration["EmailSettings:FromEmail"];
                var fromName = _configuration["EmailSettings:FromName"];

                using var smtp = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                var body = $@"
                <html>
                <head><meta charset='utf-8'><style>
                body {{ font-family: Arial,sans-serif; line-height:1.6; color:#333; }}
                .container {{ max-width:600px; margin:0 auto; padding:20px; }}
                .header {{ background-color:#007bff; color:white; padding:20px; text-align:center; }}
                .content {{ padding:20px; background-color:#f8f9fa; }}
                .ai-response {{ background-color:#e3f2fd; padding:15px; border-left:4px solid #2196f3; margin:20px 0; }}
                .footer {{ background-color:#343a40; color:white; padding:15px; text-align:center; font-size:12px; }}
                </style></head>
                <body><div class='container'><div class='header'><h2>Cental Rent A Car</h2></div>
                <div class='content'><h3>Sayın {toName},</h3>
                <p>Müşteri hizmetleri yanıtı:</p>
                <div class='ai-response'>{aiResponse}</div>
                <p>Gönderdiğiniz mesaj:<br>{message}</p>
                <p>Saygılarımızla,<br><strong>CQRS Rent A Car Müşteri Hizmetleri</strong></p></div>
                <div class='footer'><p>Bu e-posta otomatik gönderilmiştir.</p></div></div></body></html>";

                var mail = new MailMessage { From = new MailAddress(fromEmail, fromName), Subject = subject, Body = body, IsBodyHtml = true, BodyEncoding = Encoding.UTF8 };
                mail.To.Add(toEmail);

                await smtp.SendMailAsync(mail);
                _logger.LogInformation($"Mail gönderildi: {toEmail}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Mail gönderilemedi: {toEmail}");
                return false;
            }
        }
    }
}
