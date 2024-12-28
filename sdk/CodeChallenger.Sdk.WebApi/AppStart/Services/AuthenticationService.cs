namespace CodeChallenger.Sdk.WebApi.AppStart.Services
{
    using CodeChallenger.Sdk.WebApi.Attributes;
    using CodeChallenger.Sdk.WebApi.Authentication.Schemes.BasicAuthentication;
    using CodeChallenger.Sdk.WebApi.Authentication.Schemes.InternalAuthentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    public static class AuthenticationService
    {
        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            var useDefaultAuthentication = true;

            if (!string.IsNullOrEmpty(builder.Configuration["authentication:useDefaultAuthentication"])
                && bool.TryParse(builder.Configuration["authentication:useDefaultAuthentication"], out _))
            {
                useDefaultAuthentication = bool.Parse(builder.Configuration["authentication:useDefaultAuthentication"]!);
            }

            var authBuilder = builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = "MultiAuthScheme";
                x.DefaultChallengeScheme = "MultiAuthScheme";
            });

            if (useDefaultAuthentication)
            {
                var issuer = builder.Configuration["authentication:issuer"];

                var audiences = builder.Configuration["authentication:audience"]?.Split(',')
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => x.Trim())
                    .ToList() ?? [];

                var tokenKey = builder.Configuration["authentication:key"]
                    ?? throw new ArgumentNullException("authentication:key");

                authBuilder.AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudiences = audiences,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey))
                    };
                });
            }

            authBuilder
                .AddScheme<BasicAuthSchemeOptions, BasicAuthSchemeHandler>(BasicAuthDefaults.AuthenticationScheme, opt => { })
                .AddScheme<InternalAuthSchemeOptions, InternalAuthSchemeHandler>(InternalAuthDefaults.AuthenticationScheme, opt => { })
                .AddPolicyScheme("MultiAuthScheme", JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.ForwardDefaultSelector = context =>
                    {
                        var endpoint = context.GetEndpoint();

                        var basicAuthAttribute = endpoint?.Metadata.OfType<BasicAuthorizationAttribute>().FirstOrDefault();

                        if (basicAuthAttribute != null)
                        {
                            return BasicAuthDefaults.AuthenticationScheme;
                        }

                        var internalAuthAttribute = endpoint?.Metadata.OfType<InternalAuthorizationAttribute>().FirstOrDefault();

                        if (internalAuthAttribute != null)
                        {
                            return InternalAuthDefaults.AuthenticationScheme;
                        }

                        return JwtBearerDefaults.AuthenticationScheme;
                    };
                });

            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy(BasicAuthDefaults.AuthenticationScheme,
                    new AuthorizationPolicyBuilder(BasicAuthDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build());

                opt.AddPolicy(InternalAuthDefaults.AuthenticationScheme,
                    new AuthorizationPolicyBuilder(InternalAuthDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build());

                // User this code snippet if you want to run
                // an auth pipeline where user is authorized
                // if at least one method runs valid
                /*
                var defaultPolity = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme,
                    BasicAuthDefaults.AuthenticationScheme,
                    ApiKeyAuthDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser();

                opt.DefaultPolicy = defaultPolity.Build();
                */
            });
        }
    }
}