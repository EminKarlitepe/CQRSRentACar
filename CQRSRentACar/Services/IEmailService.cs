namespace CQRSRentACar.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string toName, string subject, string message, string aiResponse);
    }
}
