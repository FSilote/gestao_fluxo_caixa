namespace CodeChallenger.Lancamentos.WebApi.AppStart.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class CorsMiddleware
    {
        public static void ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors("EnableCorsPolicy");
        }
    }
}
