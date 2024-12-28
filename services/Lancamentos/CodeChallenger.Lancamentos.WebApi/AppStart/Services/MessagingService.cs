namespace CodeChallenger.Lancamentos.WebApi.AppStart.Services
{
    using CodeChallenger.Lancamentos.Adapters.RabbitMq;
    using CodeChallenger.Lancamentos.Adapters.RabbitMq.Connection;
    using CodeChallenger.Lancamentos.Domain.Messaging;
    using System.Diagnostics;

    public static class MessagingService
    {
        public static void ConfigureMessaging(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Loading Messaging...");

            builder.Services.AddScoped<IRabbitConnectionService, RabbitConnectionService>();
            builder.Services.AddScoped<IPublisherService, PublisherService>();
            builder.Services.AddScoped<IQueueService, QueueService>();
        }
    }
}
