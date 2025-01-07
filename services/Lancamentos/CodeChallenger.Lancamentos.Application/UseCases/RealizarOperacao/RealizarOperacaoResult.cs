namespace CodeChallenger.Lancamentos.Application.UseCases.RealizarOperacao
{
    using System;

    public class RealizarOperacaoResult
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public Guid Identificador { get; set; }
        public DateTime? DataRealizacao { get; set; }
    }
}
