namespace CodeChallenger.Saldo.Adapters.Repository.Mappings
{
    using CodeChallenger.Saldo.Domain.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OperacaoMapping : IEntityTypeConfiguration<Operacao>
    {
        public void Configure(EntityTypeBuilder<Operacao> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(x => x.NumeroParcela).IsRequired();
            builder.Property(x => x.TotalParcelas).IsRequired();
            builder.Property(x => x.DataCriacao).IsRequired();
            builder.Property(x => x.DataRealizacao).IsRequired();
            builder.Property(x => x.Identificador).IsRequired();
            builder.Property(x => x.Movimento).IsRequired();
            builder.Property(x => x.ValorParcela).IsRequired();
            builder.Property(x => x.ValorTotal).IsRequired();
            builder.Property(x => x.Comentario);
        }
    }
}
