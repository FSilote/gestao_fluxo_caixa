namespace CodeChallenger.Lancamentos.WebApi.AppStart.Services
{
    using CodeChallenger.Lancamentos.Domain.Encryption;
    using CodeChallenger.Lancamentos.WebApi.Encryption;
    using System.Diagnostics;

    public static class EncryptionService
    {
        public static void ConfigureEncryption(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Loading Encryption Services...");

            builder.Services.AddScoped<ISha512Service, Sha512Service>();
        }
    }
}
