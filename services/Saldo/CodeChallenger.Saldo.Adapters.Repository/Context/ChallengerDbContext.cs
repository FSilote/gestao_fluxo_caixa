namespace CodeChallenger.Saldo.Adapters.Repository.Context
{
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class ChallengerDbContext : DbContext
    {
        public ChallengerDbContext(DbContextOptions<ChallengerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
