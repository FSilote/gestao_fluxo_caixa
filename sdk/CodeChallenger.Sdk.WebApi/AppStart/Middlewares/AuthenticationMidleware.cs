namespace CodeChallenger.Sdk.WebApi.AppStart.Middlewares
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public static class AuthenticationMidleware
    {
        public static void ConfigureAuthentication(this IApplicationBuilder app)
        {
            if (app is not WebApplication webApplication)
                return;

            var configuration = webApplication.Configuration;

            if (configuration.GetSection("authentication").Exists())
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }
        }
    }
}
