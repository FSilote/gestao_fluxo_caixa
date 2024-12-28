namespace CodeChallenger.Lancamentos.WebApi.AppStart.Services
{
    using CodeChallenger.Lancamentos.Domain.Auth;
    using CodeChallenger.Lancamentos.WebApi.Auth;
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
