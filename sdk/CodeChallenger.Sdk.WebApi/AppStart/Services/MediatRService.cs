namespace CodeChallenger.Sdk.WebApi.AppStart.Services
{
    using CodeChallenger.Sdk.WebApi.AppStart.Bootstrap;
    // using Entity = Amx.Manager.Infrastructure.Data.EntityFramework.Query.v1.Handler.Common;
    // using NHibernate = Amx.Manager.Infrastructure.Data.NHibernate.Query.v1.Handler.Common;
    using Autofac;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using System;
    using System.Diagnostics;
    using System.Linq;

    public static class MediatRService
    {
        public static void ConfigureMediatR(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Loading MediatR...");

            try
            {
                var assemblies = AssemblyLoader.GetAssemblies();

                if (assemblies.Any())
                {
                    builder.Services.AddMediatR(conf =>
                    {
                        conf.RegisterServicesFromAssemblies([.. assemblies]);
                    });
                }
            }
            catch (Exception e)
            {
                Log.Logger.Information(e, "Cannot load assemblies to register MediatR.");
                Debug.WriteLine("Cannot load assemblies to register MediatR.");
                throw;
            }
        }

        public static void ConfigureMediatR(this IServiceCollection services)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Loading MediatR...");

            try
            {
                var assemblies = AssemblyLoader.GetAssemblies();

                if (assemblies.Any())
                {
                    services.AddMediatR(conf =>
                    {
                        conf.RegisterServicesFromAssemblies(assemblies.ToArray());
                    });
                }
            }
            catch (Exception e)
            {
                Log.Logger.Fatal(e, "Cannot load assemblies to register MediatR.");
                Debug.WriteLine("Cannot load assemblies to register MediatR.");
            }
        }

        public static void RegisterGenericHandlers(ContainerBuilder builder, IConfiguration configuration)
        {
            /*if (configuration.GetSection("entityFramework").Exists())
            {
                builder.RegisterGeneric(typeof(Entity.GetAllKeyValueFromEntityQueryHandler<>))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }

            if (configuration.GetSection("hibernate").Exists())
            {
                builder.RegisterGeneric(typeof(NHibernate.GetAllKeyValueFromEntityQueryHandler<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            }

            builder.RegisterGeneric(typeof(GetAllKeyValueFromEnumHandler<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();*/
        }
    }
}
