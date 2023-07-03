using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.EmailAuthentication.Application.Interfaces;
using ModsenOnlineStore.EmailAuthentication.Domain;

namespace ModsenOnlineStore.EmailAuthentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailAuthenticationController : ControllerBase
    {
        private readonly IEmailSendingService emailSendingService;
        private readonly IVerificationCodeGeneratior codeGeneratior;

        public EmailAuthenticationController(
            IEmailSendingService emailSendingService,
            IVerificationCodeGeneratior codeGeneratior) 
        {
            this.emailSendingService = emailSendingService;
            this.codeGeneratior = codeGeneratior;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync(string email)
        {
            string code = codeGeneratior.GenerateCode();

            string text = Constants.TextTitle + $"<h2>{code}</h2>" + Constants.TextBody;

            emailSendingService.SendEmail(email, Constants.Theme, text);

            return Ok(code);
        }
    }
}
