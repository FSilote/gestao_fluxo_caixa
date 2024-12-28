namespace CodeChallenger.Saldo.Adapters.RabbitMq.Connection
{
    using CodeChallenger.Saldo.Adapters.RabbitMq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using RabbitMQ.Client;
    using System.Threading.Tasks;

    public class RabbitConnectionService : IRabbitConnectionService
    {
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Include
        };

        public Task<IConnection> CreateConnectionAsync()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            return factory.CreateConnectionAsync();
        }

        public async Task<IChannel> CreateChannelAsync(IConnection connection)
        {
            var channel = await connection.CreateChannelAsync();
            
            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

            return channel;
        }
    }
}
