using System.Net.Mail;

namespace Phone_Market.Services.EmailImplementation
{
    public interface IEmailSender
    {
        Task SendDocument(string email, string subject, string message, string path);
        Task SendEmailAsync(string email, string subject, string message);
        Task SendPdf(MailMessage mailMessage);
    }
}
