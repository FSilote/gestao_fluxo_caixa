namespace CodeChallenger.Sdk.WebApi.AppStart.Services
{
    using Autofac;
    using AutofacSerilogIntegration;
    using Microsoft.AspNetCore.Builder;
    using Serilog;
    using System;
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
            }
            catch (Exception e)
            {
                Log.Logger.Information(e, "Cannot load assemblies to register SeriLog.");
                Debug.WriteLine("Cannot load assemblies to register SeriLog.");
                throw;
            }
        }

        public static void RegisterLogger(ContainerBuilder cb)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Registering SeriLog With Autofac...");
            cb.RegisterLogger(Log.Logger);
        }
    }
}
