namespace CodeChallenger.Lancamentos.Domain.Entity
{
    using CodeChallenger.Saldo.Domain.Entity;

    public class ContaCorrente : AbstractBaseEntity
    {
        public virtual decimal Saldo { get; set; } = decimal.Zero;
        public virtual DateTime DataAlteracao { get; set; } = DateTime.UtcNow;

        public virtual ContaCorrente Creditar(decimal valor)
        {
            this.Saldo += valor;
            this.DataAlteracao = DateTime.UtcNow;

            return this;
        }

        public virtual ContaCorrente Debitar(decimal valor)
        {
            this.Saldo -= valor;
            this.DataAlteracao = DateTime.UtcNow;

            return this;
        }
    }
}
