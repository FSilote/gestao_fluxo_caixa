namespace CodeChallenger.Lancamentos.Application.UseCases.RealizarOperacaoParcelada
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using MediatR;

    public class RealizarOperacaoParceladaCommand : IRequest<IEnumerable<RealizarOperacaoParceladaResult>>
    {
        public decimal Valor { get; set; }
        private Movimento Movimento { get; set; }
        public Categoria Categoria { get; set; }
        public int TotalParcelas { get; set; }
        public DateTime DataPrimeiraParcela { get; set; }
        public string? Descricao { get; set; }

        public RealizarOperacaoParceladaCommand SetMovimentoOperacao(Movimento movimento)
        {
            Movimento = movimento;
            return this;
        }

        public Movimento GetMovimentoOperacao()
        {
            return Movimento;
        }
    }
}
