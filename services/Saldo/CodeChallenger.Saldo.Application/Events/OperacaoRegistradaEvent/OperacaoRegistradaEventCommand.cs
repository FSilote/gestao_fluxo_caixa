namespace CodeChallenger.Lancamentos.Application.Events.OperacaoRegistradaEvent
{
    using CodeChallenger.Lancamentos.Application.Events.Base;
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Entity;
    using System;

    public class OperacaoRegistradaEventCommand : BaseEventCommand
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal ValorTotal { get; set; }
        public Movimento Movimento { get; set; }
        public Categoria Categoria { get; set; }
        public Guid Identificador { get; set; }
        public int TotalParcelas { get; set; }
        public bool OperacaoParcelada { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime? DataRealizacao { get; set; }
        public string? Descricao { get; set; }
        public StatusOperacao Status { get; set; }
    }
}
