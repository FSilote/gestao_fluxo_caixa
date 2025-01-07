namespace CodeChallenger.Lancamentos.Domain.Messaging
{
    using CodeChallenger.Lancamentos.Domain.Messaging.Models;

    public interface IQueueService
    {
        Task<CreateQueueResponse> CreateQueue(string queueName);
        Task<CreateQueueResponse> CreateQueue(string queueName, string exchangeNameToSubscribe);
        Task Subscribe(string topicName, string queueName);
        Task ReceiveAsync(string queueName, Func<string, Task<bool>> callback, CancellationToken cancellationToken);
    }
}
