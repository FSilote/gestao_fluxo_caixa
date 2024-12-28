namespace CodeChallenger.Lancamentos.Application.UseCases.RecuperarOperacao
{
    using MediatR;

    public class RecuperarOperacaoQueryCommand : IRequest<RecuperarOperacaoQueryResult?>
    {
        public RecuperarOperacaoQueryCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
