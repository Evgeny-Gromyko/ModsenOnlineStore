using Microsoft.Extensions.Hosting;
using ModsenOnlineStore.Common.Interfaces;
using ModsenOnlineStore.Login.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using ModsenOnlineStore.Login.Application.Services;

namespace ModsenOnlineStore.EmailAuthentication.Infrastructure.Services
{
    public class RabbitMQBackgroundConsumerService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly IRabbitMQMessagingService rabbitMQMessagingService;


        public RabbitMQBackgroundConsumerService(IServiceProvider serviceProvider, IRabbitMQMessagingService rabbitMQMessagingService)
        {
            this.serviceProvider = serviceProvider;
            this.rabbitMQMessagingService = rabbitMQMessagingService;
            var factory = new ConnectionFactory(){HostName = "rabbitmq"};
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                stoppingToken.ThrowIfCancellationRequested();

                await Task.Run(() =>
                {
                    channel.QueueDeclare(queue: "user-payment",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += async (sender, e) =>
                    {
                        var body = e.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());


                        var userId = int.Parse(message.Split()[0]);
                        var totalPrice = Decimal.Parse(message.Split()[1]);
                        var email = message.Split()[2];


                        var transientService = scope.ServiceProvider.GetRequiredService<UserMoneyService>();

                        var result = await transientService.MakePaymentAsync(userId, totalPrice);


                        var newMessage = "Payment ";

                        if (result.Success) newMessage += "succeed " + email;
                        else newMessage += "rejected " + email;

                        rabbitMQMessagingService.PublishMessage("email-confirmation", newMessage);

                    };

                    channel.BasicConsume(queue: "user-payment",
                        autoAck: true,
                        consumer: consumer);

                });
            }
        }
    }
}
