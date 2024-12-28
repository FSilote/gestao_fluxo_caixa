namespace CodeChallenger.Sdk.WebApi.AppStart.Middlewares
{
    using Asp.Versioning.ApiExplorer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Swashbuckle.AspNetCore.SwaggerUI;

    public static class SwaggerMiddleware
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            if (app is not WebApplication webApplication)
                return;

            var configuration = webApplication.Configuration;
            var provider = webApplication.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            var isDevelopment = webApplication.Environment.IsDevelopment();

            if (!isDevelopment)
                return;

            var url = configuration != null && !string.IsNullOrEmpty(configuration["swagger:url"])
                ? configuration["swagger:url"]
                : "api/swagger";

            if (url!.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1);
            }

            if (url!.StartsWith("/"))
            {
                url = url.Remove(0);
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = url + "/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                foreach (var version in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"./{version.GroupName}/swagger.json", version.GroupName);
                }

                c.DocExpansion(DocExpansion.List);
                c.RoutePrefix = url;
            });
        }
    }
}
