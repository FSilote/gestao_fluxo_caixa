namespace CodeChallenger.Sdk.WebApi.AppStart.Middlewares
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public static class EndpointsMiddleware
    {
        public static void ConfigureEndpoints(this IApplicationBuilder app)
        {
            if (app is not WebApplication webApplication)
                return;

            var configuration = webApplication.Configuration;

            var useAuthentication = configuration.GetSection("authentication").Exists();
            var validateTokens = true;

            if (!string.IsNullOrEmpty(configuration["authentication:validateJwtToken"]))
            {
                bool.TryParse(configuration["authentication:validateJwtToken"], out validateTokens);
            }

            app.UseEndpoints(e =>
            {
                if (useAuthentication && validateTokens)
                {
                    e.MapControllers().RequireAuthorization();
                }
                else
                {
                    e.MapControllers();
                }
            });
        }
    }
}
