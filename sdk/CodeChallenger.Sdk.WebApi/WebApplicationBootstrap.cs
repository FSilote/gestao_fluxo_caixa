namespace CodeChallenger.Sdk.WebApi
{
    using CodeChallenger.Sdk.WebApi.AppStart.Hosting;
    using CodeChallenger.Sdk.WebApi.AppStart.Middlewares;
    using CodeChallenger.Sdk.WebApi.AppStart.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class WebApplicationBootstrap
    {
        public static void Startup(string[] args,
            Action<IServiceCollection, IConfigurationManager> servicesConfig = null!,
            Action<WebApplication> appConfig = null!,
            Action<FilterCollection> filtersConfig = null!)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Autofac as IoC Container
            builder.ConfigureApplicationContainer();

            // Configure Default Services
            builder.ConfigureSeriLog();
            builder.ConfigureMediatR();
            builder.ConfigureAutoMapper();
            builder.ConfigureAuthentication();
            builder.ConfigureCors();
            builder.ConfigureMvc(filtersConfig);
            builder.ConfigureVersioning();
            builder.ConfigureSwagger();
            builder.ConfigureMemoryCache();

            servicesConfig?.Invoke(builder.Services, builder.Configuration);

            // Build the WebApplication
            var app = builder.Build();

            // Configure Default Middlewares
            app.ConfigureCors();
            app.ConfigureMvc();
            app.ConfigureAuthentication();
            app.ConfigureEndpoints();
            app.ConfigureApiVersioning();
            app.ConfigureSwagger();

            appConfig?.Invoke(app);

            app.Run();
        }
    }
}
