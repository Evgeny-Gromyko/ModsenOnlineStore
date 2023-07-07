namespace ModsenOnlineStore.Common.Interfaces
{
    public interface IRabbitMQMessagingService
    {
        void PublishMessage(string queue, string message);
    }
}
