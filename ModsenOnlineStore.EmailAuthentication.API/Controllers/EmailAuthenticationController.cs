using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.EmailAuthentication.Application.Interfaces;
using System.Net.Mail;

namespace ModsenOnlineStore.EmailAuthentication.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailAuthenticationController : ControllerBase
    {
        private IEmailSendingService emailSendingService;
        private IVerificationCodeGeneratior codeGeneratior;
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
            string code = codeGeneratior.generateCode();

            string title = "Online store verification code";

            string text = $"<h2>Here is your verification code : {code}</h2>" +
                           "<p>copy it and paste it into the field for entering the verification code</p>";

            emailSendingService.SendAuthEmail(email, title, text);

            return Ok(code);
        }
    }
}
