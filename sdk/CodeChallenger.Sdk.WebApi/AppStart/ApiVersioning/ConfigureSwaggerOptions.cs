namespace CodeChallenger.Sdk.WebApi.AppStart.ApiVersioning
{
    using Asp.Versioning.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
        {
            this._provider = provider;
            this._configuration = configuration;
        }

        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IConfiguration _configuration;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = _configuration["swagger:title"] ?? "No Swagger title defined",
                Version = description.GroupName,
                Description = _configuration["swagger:description"] ?? "No Swagger description defined",
                Contact = _configuration.GetSection("swagger").GetSection("contact").Exists()
                    ? new OpenApiContact()
                    {
                        Name = _configuration["swagger:contact:name"],
                        Email = _configuration["swagger:contact:email"]
                    }
                    : null
            };

            if (description.IsDeprecated)
            {
                info.Description += " [Deprecated]";
            }

            return info;
        }
    }
}
