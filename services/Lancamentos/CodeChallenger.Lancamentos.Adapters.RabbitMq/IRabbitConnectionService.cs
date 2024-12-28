namespace CodeChallenger.Lancamentos.Adapters.RabbitMq
{
    using RabbitMQ.Client;
    using System.Threading.Tasks;

    public interface IRabbitConnectionService
    {
        Task<IConnection> CreateConnectionAsync();
        Task<IChannel> CreateChannelAsync(IConnection connection);
    }
}
