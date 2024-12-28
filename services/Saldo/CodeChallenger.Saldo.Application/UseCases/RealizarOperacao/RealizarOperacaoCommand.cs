namespace CodeChallenger.Saldo.Application.UseCases.RealizarOperacao
{
    using CodeChallenger.Saldo.Domain.Entity;
    using MediatR;

    public class RealizarOperacaoCommand : IRequest<RealizarOperacaoResult>
    {
        public decimal Valor { get; set; }
        public string? Comentario { get; set; }
        public Categoria Categoria { get; protected set; }
        public DateTime Data {  get; set; }
        
        private Movimento Movimento { get; set; }

        public RealizarOperacaoCommand SetMovimentoOperacao(Movimento movimento)
        {
            this.Movimento = movimento;
            return this;
        }

        public Movimento GetMovimentoOperacao()
        {
            return this.Movimento;
        }
    }
}
