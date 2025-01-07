namespace CodeChallenger.Saldo.Application.UseCases.RecuperarOperacao
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Entity;

    public class RecuperarOperacaoQueryResult
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal ValorTotal { get; set; }
        public Movimento Movimento { get; set; }
        public StatusOperacao Status { get; set; }
        public Guid Identificador { get; set; }
        public int TotalParcelas { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime? DataRealizacao { get; set; }
        public bool PossuiParcelamento { get => this.TotalParcelas > 1; }
        public string Descricao { get; set; } = null!;
    }
}
