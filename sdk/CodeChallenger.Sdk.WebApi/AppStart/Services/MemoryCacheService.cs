namespace CodeChallenger.Sdk.WebApi.AppStart.Services
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class MemoryCacheService
    {
        public static void ConfigureMemoryCache(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();
        }
    }
}
