namespace CodeChallenger.Saldo.Adapters.RabbitMq
{
    using CodeChallenger.Saldo.Domain.Messaging;
    using CodeChallenger.Saldo.Domain.Messaging.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class QueueService : IQueueService
    {
        public QueueService(IRabbitConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        private readonly IRabbitConnectionService _connectionService;

        public Task<CreateQueueResponse> CreateQueue(string queueName)
        {
            return this.CreateQueue(queueName, null!);
        }

        public async Task<CreateQueueResponse> CreateQueue(string queueName, string exchangeNameToSubscribe)
        {
            using (var connection = await _connectionService.CreateConnectionAsync())
            {
                using (var channel = await _connectionService.CreateChannelAsync(connection))
                {
                    var result = !string.IsNullOrEmpty(queueName)
                        ? await channel.QueueDeclareAsync(
                            queue: queueName,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null)
                        : await channel.QueueDeclareAsync();

                    if (!string.IsNullOrEmpty(exchangeNameToSubscribe))
                    {
                        await channel.QueueBindAsync(queue: result.QueueName, exchange: exchangeNameToSubscribe, routingKey: string.Empty);
                    }

                    return new CreateQueueResponse(result.QueueName, !string.IsNullOrEmpty(result.QueueName));
                }
            }
        }

        public async Task Subscribe(string exchangeName, string queueName)
        {
            using (var connection = await _connectionService.CreateConnectionAsync())
            {
                using (var channel = await _connectionService.CreateChannelAsync(connection))
                {
                    await channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: string.Empty);
                }
            }
        }

        public async Task ReceiveAsync(string queueName, Func<string, Task<bool>> callback, CancellationToken cancellationToken)
        {
            using (var connection = await _connectionService.CreateConnectionAsync())
            {
                using (var channel = await _connectionService.CreateChannelAsync(connection))
                {
                    var consumer = new AsyncEventingBasicConsumer(channel);

                    consumer.ReceivedAsync += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        var success = await callback(message);

                        if (success)
                        {
                            await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                        }
                        else
                        {
                            await channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                        }
                    };
                        
                    await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer, cancellationToken: cancellationToken);

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Sleeping 5 seconds...");
                        await Task.Delay(5000, cancellationToken);
                    }
                }
            }
        }
    }
}
