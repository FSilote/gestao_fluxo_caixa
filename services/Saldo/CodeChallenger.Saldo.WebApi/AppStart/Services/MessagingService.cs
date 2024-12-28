namespace CodeChallenger.Saldo.WebApi.AppStart.Services
{
    using CodeChallenger.Saldo.Adapters.RabbitMq;
    using CodeChallenger.Saldo.Adapters.RabbitMq.Connection;
    using CodeChallenger.Saldo.Domain.Messaging;
    using System.Diagnostics;

    public static class MessagingService
    {
        public static void ConfigureMessaging(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Loading Messaging...");

            builder.Services.AddSingleton<IRabbitConnectionService, RabbitConnectionService>();
            builder.Services.AddScoped<IPublisherService, PublisherService>();
            builder.Services.AddSingleton<IQueueService, QueueService>();
        }
    }
}
