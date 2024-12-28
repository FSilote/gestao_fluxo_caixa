namespace CodeChallenger.Saldo.WebApi.AppStart.Services
{
    using CodeChallenger.Saldo.Domain.Auth;
    using CodeChallenger.Saldo.WebApi.Auth;
    using System.Diagnostics;

    public static class TokensService
    {
        public static void ConfigureTokenServices(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Loading Token Services...");

            builder.Services.AddScoped<ITokenService, TokenService>();
        }
    }
}
