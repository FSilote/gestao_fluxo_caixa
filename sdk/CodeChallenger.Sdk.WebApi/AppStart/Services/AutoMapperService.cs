namespace CodeChallenger.Sdk.WebApi.AppStart.Services
{
    using CodeChallenger.Sdk.WebApi.AppStart.Bootstrap;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using System;
    using System.Diagnostics;
    using System.Linq;

    public static class AutoMapperService
    {
        public static void ConfigureAutoMapper(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Loading AutoMapper...");

            try
            {
                var assemblies = AssemblyLoader.GetAssemblies();

                if (assemblies.Any())
                {
                    builder.Services.AddAutoMapper(assemblies);
                }
            }
            catch (Exception e)
            {
                Log.Logger.Fatal(e, "Cannot load assemblies to register AutoMapper.");
                Debug.WriteLine("Cannot load assemblies to register AutoMapper.");
                throw;
            }
        }
    }
}
