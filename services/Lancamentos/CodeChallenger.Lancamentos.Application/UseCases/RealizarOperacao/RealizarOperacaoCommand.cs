namespace CodeChallenger.Lancamentos.Application.UseCases.RealizarOperacao
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using MediatR;

    public class RealizarOperacaoCommand : IRequest<RealizarOperacaoResult>
    {
        public decimal Valor { get; set; }
        public string? Descricao { get; set; }
        public Categoria Categoria { get; set; }
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
