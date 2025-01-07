namespace CodeChallenger.Lancamentos.Application.UseCases.ListarOperacoes
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using MediatR;

    public class ListarOperacoesQueryCommand : IRequest<IEnumerable<ListarOperacoesQueryResult>>
    {
        public Movimento? Movimento { get; set; }
        public bool? ComParcelamento { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public string? Descricao { get; set; }
    }
}
