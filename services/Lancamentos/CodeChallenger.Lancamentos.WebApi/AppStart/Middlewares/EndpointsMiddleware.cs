namespace CodeChallenger.Lancamentos.WebApi.AppStart.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class EndpointsMiddleware
    {
        public static void ConfigureEndpoints(this IApplicationBuilder app)
        {
            if (app is not WebApplication webApplication)
                return;

            var configuration = webApplication.Configuration;

            app.UseEndpoints(e =>
            {
                e.MapControllers().RequireAuthorization();
            });
        }
    }
}
