namespace CodeChallenger.Lancamentos.Application.UseCases.RealizarPagamentoParcela
{
    using CodeChallenger.Lancamentos.Domain.Entity;

    public class RealizarPagamentoParcelaResult
    {
        public int Id { get; set; }
        public StatusOperacao Status { get; set; }
    }
}
