namespace CodeChallenger.Lancamentos.Application.UseCases.RecuperarSaldoContaCorrente
{
    public class RecuperarSaldoQueryResult
    {
        public decimal Saldo { get; set; } = decimal.Zero;
        public DateTime? DataAlteracao { get; set; } = null!;
    }
}
