namespace CodeChallenger.Lancamentos.WebApi.AppStart.Services
{
    using CodeChallenger.Lancamentos.Domain.Auth;
    using CodeChallenger.Lancamentos.WebApi.Auth;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.Diagnostics;
    using System.Text;

    public static class AuthorizationService
    {
        public static void ConfigureAuthorization(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Configuring Authorization Services...");

            var tokenKey = builder.Configuration["authentication:jwtKey"]
                ?? throw new Exception("JWT Token key not found.");

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("Gerente", p => p.RequireRole("Gerente"));
                x.AddPolicy("Atendente", p => p.RequireRole("Gerente", "Atendente"));
            });
        }
    }
}
