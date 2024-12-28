namespace CodeChallenger.Sdk.WebApi.AppStart.Services
{
    using Asp.Versioning;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApiVersioningService
    {
        public static void ConfigureVersioning(this WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new MediaTypeApiVersionReader("api-version"),
                    new HeaderApiVersionReader()
                    {
                        HeaderNames = { "x-api-version" }
                    });
            })
            .AddApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VV";
                p.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
