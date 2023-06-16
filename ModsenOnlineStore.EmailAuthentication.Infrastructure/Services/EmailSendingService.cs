using Microsoft.Extensions.Configuration;
using ModsenOnlineStore.EmailAuthentication.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.EmailAuthentication.Infrastructure.Services
{
    public class EmailSendingService : IEmailSendingService
    {

        private MailAddress senderAddress;
        private NetworkCredential networkCredential;

        public EmailSendingService(IConfiguration configuration) {
            senderAddress = new MailAddress(configuration["Credentials:Email"], "Store Mail Authentication");
            networkCredential = new NetworkCredential(configuration["Credentials:Email"], configuration["Credentials:Password"]);
        }
        
        public void SendEmail(string mailAddress = "egrom2002@gmail.com", string title = "", string text = "")
        {
            var receiverAddress = new MailAddress(mailAddress);
            var message = new MailMessage(senderAddress, receiverAddress);

            message.Subject = title;
            message.Body = text;
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient() // gmail smtp settings
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = networkCredential 
            };
            smtp.Send(message);
        }

    }
}
