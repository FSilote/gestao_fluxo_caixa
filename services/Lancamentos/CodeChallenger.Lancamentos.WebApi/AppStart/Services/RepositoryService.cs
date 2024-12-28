namespace CodeChallenger.Lancamentos.WebApi.AppStart.Services
{
    using Amx.Storage.Infrastructure.Data.EntityFramework;
    using CodeChallenger.Lancamentos.Adapters.Repository.Context;
    using CodeChallenger.Lancamentos.Domain.Repository;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics;
    public static class RepositoryService
    {
        public static void ConfigureRepository(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Loading Repository...");

            builder.Services.AddDbContext<ChallengerDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("challenger_database");
            });

            builder.Services.AddScoped<IReadRepository, ReadRepositoryEntityFramework>();
            builder.Services.AddScoped<IWriteRepository, WriteRepositoryEntityFramework>();
        }
    }
}
