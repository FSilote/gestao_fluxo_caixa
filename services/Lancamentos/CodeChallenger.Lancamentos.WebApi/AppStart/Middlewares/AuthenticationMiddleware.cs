namespace CodeChallenger.Lancamentos.WebApi.AppStart.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class AuthenticationMiddleware
    {
        public static void ConfigureAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
