namespace CodeChallenger.Saldo.Domain.Messaging
{
    using System.Threading.Tasks;

    public interface IPublisherService
    {
        Task Publish(string topicName, object message);
        Task Publish(string topicName, IEnumerable<object> messages);
        Task Subscribe(string topicName, string queueName);
    }
}
