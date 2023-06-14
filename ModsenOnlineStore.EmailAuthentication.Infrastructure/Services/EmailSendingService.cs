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
        private MailAddress from = new MailAddress("akkdliabreda@gmail.com", "Store Mail Authentication");
        private NetworkCredential networkCredential = new NetworkCredential("akkdliabreda@gmail.com", "vqshkhtemsppmhha");

        public void SendAuthEmail(string mailAddress = "egrom2002@gmail.com", string title = "", string text = "")
        {
            MailAddress to = new MailAddress(mailAddress);
            MailMessage m = new MailMessage(from, to);

            m.Subject = title;
            m.Body = text;
            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient() // gmail smtp settings
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = networkCredential 
            };
            smtp.Send(m);
        }

    }
}
