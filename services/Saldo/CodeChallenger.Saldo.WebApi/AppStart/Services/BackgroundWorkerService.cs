namespace CodeChallenger.Saldo.WebApi.AppStart.Services
{
    using CodeChallenger.Saldo.WebApi.Background;
    using System.Diagnostics;

    public static class BackgroundWorkerService
    {
        public static void ConfigureBackgroundServices(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Configuring Background Services...");

            builder.Services.AddHostedService<ConsumerBackgroundService>();
        }
    }
}
