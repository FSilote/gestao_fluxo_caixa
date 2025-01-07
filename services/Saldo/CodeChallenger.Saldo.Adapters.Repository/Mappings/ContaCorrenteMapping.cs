namespace CodeChallenger.Saldo.Adapters.Repository.Mappings
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ContaCorrenteMapping : IEntityTypeConfiguration<ContaCorrente>
    {
        public void Configure(EntityTypeBuilder<ContaCorrente> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(x => x.Saldo).IsRequired();
            builder.Property(x => x.DataCriacao).IsRequired();
            builder.Property(x => x.DataAlteracao);
        }
    }
}
