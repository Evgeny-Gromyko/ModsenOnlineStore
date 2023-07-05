namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface IRabbitMQMessagingService
    {
        void PublishMessage(string queue, string message);
    }
}
