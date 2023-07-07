using ModsenOnlineStore.Common.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace ModsenOnlineStore.Common.Services
{
    public class RabbitMQMessagingService : IRabbitMQMessagingService
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        public RabbitMQMessagingService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void PublishMessage(string queue, string message)
        {
            channel.QueueDeclare(queue: queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queue,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
