namespace CodeChallenger.Lancamentos.Application.UseCases.RealizarOperacaoParcelada
{
    using System;

    public class RealizarOperacaoParceladaResult
    {
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public Guid Identificador { get; set; }
        public int TotalParcelas { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime? DataRealizacao { get; set; }
    }
}
