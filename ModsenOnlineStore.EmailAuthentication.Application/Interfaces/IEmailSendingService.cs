using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.EmailAuthentication.Application.Interfaces
{
    public interface IEmailSendingService
    {
        public void SendEmail(string mailAddress = "egrom2002@gmail.com", string title = "", string text = "");
    }
}
