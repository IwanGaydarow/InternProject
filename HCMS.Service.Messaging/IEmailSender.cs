namespace HCMS.Service.Messaging
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IEmailSender
    {
        Task SendEmailAsync(
            string from,
            string fromName,
            string to,
            string subject,
            string htmlContent,
            IEnumerable<EmailAttachment> attachments = null);
    }
}
