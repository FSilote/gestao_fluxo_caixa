namespace CodeChallenger.Sdk.WebApi.AppStart.Hosting
{
    using CodeChallenger.Sdk.WebApi.AppStart.Bootstrap;
    using CodeChallenger.Sdk.WebApi.AppStart.Services;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System.Linq;

    public static class AutofacContainerHosting
    {
        public static void ConfigureApplicationContainer(this WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                RegisterModules(cb, builder.Configuration);
                MediatRService.RegisterGenericHandlers(cb, builder.Configuration);
                SeriLogService.RegisterLogger(cb);
            });
        }

        public static void ConfigureApplicationContainer(this IHostBuilder builder, IConfiguration configuration)
        {
            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.ConfigureContainer<ContainerBuilder>(builder =>
            {
                RegisterModules(builder, configuration);
                MediatRService.RegisterGenericHandlers(builder, configuration);
            });
        }

        private static void RegisterModules(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            var assemblies = AssemblyLoader.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Module)))
                    .ToList();

                foreach (var type in types)
                {
                    var constructor = type.GetConstructors().FirstOrDefault();

                    var parameters = constructor!.GetParameters().Length > 0
                        ? [configuration]
                        : Array.Empty<object>();

                    if (constructor.Invoke(parameters) is Module module)
                        containerBuilder.RegisterModule(module);
                }
            }
        }
    }
}
