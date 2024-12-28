namespace CodeChallenger.Lancamentos.WebApi.AppStart.Services
{
    using Serilog;
    using System.Diagnostics;
    public static class SeriLogService
    {
        public static void ConfigureSeriLog(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Loading SeriLog...");

            try
            {
                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .CreateLogger();

                Log.Logger = logger;

                builder.Host.UseSerilog((context, configuration) =>
                    configuration.ReadFrom.Configuration(context.Configuration));

                builder.Services.AddSerilog(Log.Logger);
            }
            catch (Exception e)
            {
                Log.Logger.Information(e, "Cannot load assemblies to register SeriLog.");
                Debug.WriteLine("Cannot load assemblies to register SeriLog.");
                throw;
            }
        }
    }
}
