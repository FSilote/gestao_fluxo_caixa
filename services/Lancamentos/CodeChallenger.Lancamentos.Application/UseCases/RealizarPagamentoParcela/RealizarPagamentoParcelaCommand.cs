namespace CodeChallenger.Lancamentos.Application.UseCases.RealizarPagamentoParcela
{
    using MediatR;

    public class RealizarPagamentoParcelaCommand : IRequest<RealizarPagamentoParcelaResult>
    {
        public RealizarPagamentoParcelaCommand(int id)
        {
            this.IdParcela = id;
        }

        public int IdParcela { get; set; }
    }
}
