namespace CodeChallenger.Lancamentos.Adapters.RabbitMq
{
    using CodeChallenger.Lancamentos.Domain.Messaging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using RabbitMQ.Client;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class PublisherService : IPublisherService
    {
        public PublisherService(IRabbitConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        private readonly IRabbitConnectionService _connectionService;

        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Include
        };

        public Task Publish(string topicName, object message)
        {
            return this.Publish(topicName, [message]);
        }

        public async Task Publish(string topicName, IEnumerable<object> messages)
        {
            if (messages is null)
                return;

            using (var connection = await _connectionService.CreateConnectionAsync())
            {
                using (var channel = await _connectionService.CreateChannelAsync(connection))
                {
                    foreach (var message in messages)
                    {
                        var serialized = message is string
                            ? message as string
                            : JsonConvert.SerializeObject(message, _jsonSettings);

                        var body = Encoding.UTF8.GetBytes(serialized!);

                        await channel.ExchangeDeclareAsync(exchange: topicName, type: ExchangeType.Fanout);

                        await channel.BasicPublishAsync(
                            exchange: topicName,
                            routingKey: string.Empty,
                            mandatory: false,
                            basicProperties: new BasicProperties { Persistent = true },
                            body: body);
                    }
                }
            }
        }

        public async Task Subscribe(string topicName, string queueName)
        {
            using (var connection = await _connectionService.CreateConnectionAsync())
            {
                using (var channel = await _connectionService.CreateChannelAsync(connection))
                {
                    await channel.QueueBindAsync(queue: queueName, exchange: topicName, routingKey: string.Empty);
                }
            }
        }
    }
}
