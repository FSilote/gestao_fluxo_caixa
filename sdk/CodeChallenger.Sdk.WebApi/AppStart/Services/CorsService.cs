namespace CodeChallenger.Sdk.WebApi.AppStart.Services
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class CorsService
    {
        public static void ConfigureCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(o => o.AddPolicy("EnableCorsPolicy", _builder =>
            {
                _builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(x => true);
            }));
        }
    }
}
