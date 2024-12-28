namespace CodeChallenger.Saldo.Domain.Messaging
{
    using CodeChallenger.Saldo.Domain.Messaging.Models;

    public interface IQueueService
    {
        Task<CreateQueueResponse> CreateQueue(string queueName);
        Task Subscribe(string topicName, string queueName);
        Task ReceiveAsync(string queueName, Action<string> callback);
        Task ReceiveAsync<T>(string queueName, Action<T> callback);
    }
}
