using Phone_Market.Code;
using System.Net.Mail;

namespace Phone_Market.Services.EmailImplementation
{
    public class EmailService : BaseService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(ServiceDependencies serviceDependencies, IEmailSender emailSender) : base(serviceDependencies)
        {
            _emailSender = emailSender;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await _emailSender.SendEmailAsync(email, subject, message);
        }

        public async Task SendPdf(MailMessage mailMessage)
        {
            await _emailSender.SendPdf(mailMessage);
        }

        public async Task SendDocument(string email, string subject, string message, string path)
        {
            await _emailSender.SendDocument(email, subject, message, path);
        }

    }
}
