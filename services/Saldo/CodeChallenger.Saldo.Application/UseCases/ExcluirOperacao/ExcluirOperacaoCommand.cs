namespace CodeChallenger.Saldo.Application.UseCases.ExcluirOperacao
{
    using MediatR;

    public class ExcluirOperacaoCommand : IRequest<ExcluirOperacaoResult>
    {
        public ExcluirOperacaoCommand(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
