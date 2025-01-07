namespace CodeChallenger.Saldo.WebApi.Background
{
    using CodeChallenger.Lancamentos.Application.Events;
    using CodeChallenger.Saldo.Domain.Messaging;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading;

    public class ConsumerBackgroundService : BackgroundService
    {
        public ConsumerBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _scopeFactory = serviceScopeFactory;
        }

        private readonly IServiceScopeFactory _scopeFactory;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var workerScope = _scopeFactory.CreateScope())
            {
                var _queueService = workerScope.ServiceProvider.GetRequiredService<IQueueService>();
                var _publisherService = workerScope.ServiceProvider.GetRequiredService<IPublisherService>();
                var _commandFactory = workerScope.ServiceProvider.GetRequiredService<IEventCommandFactory>();

                await _publisherService.CreateTopic(TopicNames.OPERACOES);
                await _queueService.CreateQueue(QueueNames.OPERACOES, TopicNames.OPERACOES);

                await _queueService.ReceiveAsync(QueueNames.OPERACOES, async (message) =>
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        var command = _commandFactory.Create(message);

                        if (command != null)
                        {
                            return await _mediator.Send(command);
                        }

                        return false;
                    }
                }, stoppingToken);
            }
        }
    }
}
