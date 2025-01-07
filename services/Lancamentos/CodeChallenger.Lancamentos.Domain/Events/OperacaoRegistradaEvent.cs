﻿namespace CodeChallenger.Lancamentos.Domain.Events
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Lancamentos.Domain.Events.Base;

    public class OperacaoRegistradaEvent: AbstractBaseEvent
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal ValorTotal { get; set; }
        public Movimento Movimento { get; set; }
        public Guid Identificador { get; set; }
        public int TotalParcelas { get; set; }
        public bool OperacaoParcelada => this.TotalParcelas > 1;
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string? Comentario { get; set; }
    }
}
