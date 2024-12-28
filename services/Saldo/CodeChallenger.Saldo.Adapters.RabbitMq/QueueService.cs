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

        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Include
        };

        public async Task<CreateQueueResponse> CreateQueue(string queueName)
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

        public async Task ReceiveAsync(string queueName, Action<string> callback)
        {
            using (var connection = await _connectionService.CreateConnectionAsync())
            {
                using (var channel = await _connectionService.CreateChannelAsync(connection))
                {
                    var consumer = new AsyncEventingBasicConsumer(channel);

                    consumer.ReceivedAsync += async (model, ea) =>
                    {
                        byte[] body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        callback(message);

                        await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer);
                }
            }
        }

        public async Task ReceiveAsync<T>(string queueName, Action<T> callback)
        {
            using (var connection = await _connectionService.CreateConnectionAsync())
            {
                using (var channel = await _connectionService.CreateChannelAsync(connection))
                {
                    var consumer = new AsyncEventingBasicConsumer(channel);

                    consumer.ReceivedAsync += async (model, ea) =>
                    {
                        byte[] body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        var obj = JsonConvert.DeserializeObject<T>(message);

                        if (obj != null)
                        {
                            callback(obj);
                        }

                        await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    await channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer);
                }
            }
        }
    }
}
