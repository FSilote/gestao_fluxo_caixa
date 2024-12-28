namespace CodeChallenger.Saldo.WebApi.AppStart.Middlewares
{
    using CodeChallenger.Saldo.WebApi.Middewares;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;

    public static class MvcMiddleware
    {
        public static void ConfigureMvc(this IApplicationBuilder app)
        {
            if (app is not WebApplication webApplication)
                return;

            var configuration = webApplication.Configuration;
            var isDevelopment = webApplication.Environment.IsDevelopment();

            if (isDevelopment)
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

            app.UseHealthChecks("/health");
            app.UseStatusCodePages();
            app.UseRouting();
        }
    }
}
