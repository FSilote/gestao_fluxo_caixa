namespace CodeChallenger.Saldo.WebApi.AppStart.Services
{
    using CodeChallenger.Lancamentos.Application.Events;
    using System.Diagnostics;

    public static class FactoryService
    {
        public static void ConfigureEventFactory(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Configuring Event Command Factory...");

            builder.Services.AddScoped<IEventCommandFactory, EventCommandFactory>();
        }
    }
}
