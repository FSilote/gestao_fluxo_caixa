namespace CodeChallenger.Saldo.Adapters.Repository.Mappings
{
    using CodeChallenger.Saldo.Domain.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Senha).IsRequired();
            builder.Property(x => x.PerfisAcesso).IsRequired();
            builder.Property(x => x.DataCriacao).IsRequired();
        }
    }
}
