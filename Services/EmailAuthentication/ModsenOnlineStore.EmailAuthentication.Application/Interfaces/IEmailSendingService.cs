namespace ModsenOnlineStore.EmailAuthentication.Application.Interfaces
{
    public interface IEmailSendingService
    {
        public void SendEmail(string mailAddress = "egrom2002@gmail.com", string title = "", string text = "");
    }
}
