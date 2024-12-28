namespace CodeChallenger.Saldo.Application.UseCases.RealizarOperacaoParcelada
{
    using CodeChallenger.Saldo.Domain.Entity;
    using MediatR;

    public class RealizarOperacaoParceladaCommand : IRequest<IEnumerable<RealizarOperacaoParceladaResult>>
    {
        public decimal Valor { get; protected set; }
        public Movimento Movimento { get; private set; }
        public int TotalParcelas { get; protected set; }
        public DateTime DataPrimeiraParcela { get; protected set; }
        public string? Comentario { get; protected set; }

        public RealizarOperacaoParceladaCommand SetMovimentoOperacao(Movimento movimento)
        {
            Movimento = movimento;
            return this;
        }
    }
}
