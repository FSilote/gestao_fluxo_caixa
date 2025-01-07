namespace CodeChallenger.Lancamentos.Application.UseCases.RealizarOperacaoParcelada
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using MediatR;

    public class RealizarOperacaoParceladaCommand : IRequest<IEnumerable<RealizarOperacaoParceladaResult>>
    {
        public decimal Valor { get; protected set; }
        public Movimento Movimento { get; private set; }
        public int TotalParcelas { get; protected set; }
        public DateTime DataPrimeiraParcela { get; protected set; }
        public string? Descricao { get; protected set; }

        public RealizarOperacaoParceladaCommand SetMovimentoOperacao(Movimento movimento)
        {
            Movimento = movimento;
            return this;
        }
    }
}
