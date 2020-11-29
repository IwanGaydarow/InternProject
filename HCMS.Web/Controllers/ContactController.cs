namespace HCMS.Web.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
        
    using HCMS.Service.Messaging;
    using HCMS.Web.ViewModels;
    public class ContactController : Controller
    {
        private readonly IEmailSender emailSender;

        public ContactController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            await this.emailSender.SendEmailAsync(
                model.From,
                model.From,
                model.To,
                model.Subject,
                model.Content);

            return this.Redirect(model.ReturnUrl);
        }
    }
}