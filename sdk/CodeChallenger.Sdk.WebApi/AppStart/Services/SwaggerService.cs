namespace CodeChallenger.Sdk.WebApi.AppStart.Services
{
    using CodeChallenger.Sdk.WebApi.AppStart.ApiVersioning;
    using CodeChallenger.Sdk.WebApi.Swagger;
    using CodeChallenger.Sdk.WebApi.Swagger.Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SwaggerService
    {
        public static void ConfigureSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            var type = typeof(AbstractSwaggerGenStrategy);

            var strategies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && type.IsAssignableFrom(t))
                .ToList();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<SwaggerIgnorePropertyFilter>();

                if (strategies?.Any() ?? false)
                {
                    foreach (var strategyType in strategies)
                    {
                        Activator.CreateInstance(strategyType, c);
                    }
                }

                if (bool.TryParse(builder.Configuration["swagger:enableAuthentication"], out bool enableAuthentication) && enableAuthentication)
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.\r\nExample: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header
                                },
                                new List<string>()
                            }
                    });
                }
            });
        }
    }
}
