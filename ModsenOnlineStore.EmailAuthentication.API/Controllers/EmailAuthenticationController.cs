using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.EmailAuthentication.Application.Interfaces;
using ModsenOnlineStore.EmailAuthentication.Domain;
using System.Net.Mail;

namespace ModsenOnlineStore.EmailAuthentication.API.Controllers
{
    [Route("[controller]")]
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
        public async Task<IActionResult> RegisterUser(string email)
        {
            string code = codeGeneratior.GenerateCode();

            string text = Constants.TextTitle + $"<h2>{code}</h2>" + Constants.TextBody;

            emailSendingService.SendEmail(email, Constants.Theme, text);

            return Ok(code);
        }
    }
}
