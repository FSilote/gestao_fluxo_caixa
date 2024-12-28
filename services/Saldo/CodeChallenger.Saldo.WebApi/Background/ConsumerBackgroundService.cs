namespace CodeChallenger.Saldo.WebApi.Background
{
    using CodeChallenger.Saldo.Domain.Messaging;
    using MediatR;
    using Serilog;

    public class ConsumerBackgroundService : BackgroundService
    {
        public ConsumerBackgroundService(IQueueService queueService,
            IMediator mediator)
        {
            _mediator = mediator;
            _queueService = queueService;
        }

        private readonly IQueueService _queueService;
        private readonly IMediator _mediator;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _queueService.CreateQueue("teste");
                    await _queueService.Subscribe(TopicNames.OPERACOES, "teste");

                    await _queueService.ReceiveAsync("teste", async (message) =>
                    {
                        Console.WriteLine(message);
                        await Task.Delay(1000);
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error to start Background service.");
                    await Task.Delay(10000);
                }
            }
        }
    }
}
