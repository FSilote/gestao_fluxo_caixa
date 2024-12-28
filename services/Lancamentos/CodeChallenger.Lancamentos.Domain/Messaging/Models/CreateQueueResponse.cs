namespace CodeChallenger.Lancamentos.Domain.Messaging.Models
{
    public class CreateQueueResponse
    {
        public CreateQueueResponse(string queueName, bool success)
        {
            QueueName = queueName;
            Success = success;
        }

        public string QueueName { get; set; }
        public bool Success { get; set; }
    }
}
