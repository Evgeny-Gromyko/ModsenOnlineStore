using Microsoft.Extensions.Hosting;
using ModsenOnlineStore.EmailAuthentication.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ModsenOnlineStore.EmailAuthentication.Infrastructure.Services
{
    public class RabbitMQBackgroundConsumerService : BackgroundService
    {
        private readonly IEmailSendingService emailSendingService;
        private readonly IConnection connection;
        private readonly IModel channel;

        public RabbitMQBackgroundConsumerService(IEmailSendingService emailSendingService)
        {
            this.emailSendingService = emailSendingService;
            var factory = new ConnectionFactory();
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            await Task.Run(() =>
            {
                channel.QueueDeclare(queue: "email-confirmation",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, e) =>
                {
                    var body = e.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());

                    var email = message.Split()[0];
                    var url = message.Split()[1];

                    emailSendingService.SendEmail(email,
                                                  Domain.Constants.EmailConfirmationTheme,
                                                  string.Format(Domain.Constants.EmailConfirmationText, url));
                };

                channel.BasicConsume(queue: "email-confirmation",
                                     autoAck: true,
                                     consumer: consumer);
            });
        }
    }
}
