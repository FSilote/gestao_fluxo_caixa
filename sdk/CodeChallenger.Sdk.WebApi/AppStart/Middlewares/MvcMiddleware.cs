namespace CodeChallenger.Sdk.WebApi.AppStart.Middlewares
{
    using CodeChallenger.Sdk.WebApi.Middlewares;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System.Globalization;

    public static class MvcMiddleware
    {
        public static void ConfigureMvc(this IApplicationBuilder app)
        {
            if (app is not WebApplication webApplication)
                return;

            var configuration = webApplication.Configuration;
            var isDevelopment = webApplication.Environment.IsDevelopment();

            var contextPath = configuration["applicationPathBase"];
            var cultures = GetCultures(configuration);
            var supportedCultures = cultures.Select(x => new CultureInfo(x)).ToArray();

            if (isDevelopment)
            {
                app.UseDeveloperExceptionPage();
            }

            if (!string.IsNullOrEmpty(contextPath))
            {
                app.UsePathBase(contextPath);
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: cultures[0], uiCulture: cultures[0]),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

            app.UseHealthChecks("/health");
            app.UseStatusCodePages();
            app.UseRouting();
        }

        private static IList<string> GetCultures(IConfiguration configuration)
        {
            var config = (configuration["cultures"] ?? string.Empty).Replace(",", ";");

            var cultures = config.Split(";").Where(x => !string.IsNullOrEmpty(x)).ToList();

            if (!cultures.Contains("en-US"))
                cultures.Add("en-US");

            return cultures;
        }
    }
}
