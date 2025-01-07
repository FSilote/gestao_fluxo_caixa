﻿namespace CodeChallenger.Lancamentos.Application.UseCases.ListarOperacoes
{
    using CodeChallenger.Lancamentos.Domain.Entity;

    public class ListarOperacoesQueryResult
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal ValorTotal { get; set; }
        public Movimento Movimento { get; set; }
        public Categoria Categoria { get; set; }
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
