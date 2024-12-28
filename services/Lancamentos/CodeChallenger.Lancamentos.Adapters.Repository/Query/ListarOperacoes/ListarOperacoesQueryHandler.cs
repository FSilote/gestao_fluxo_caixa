namespace CodeChallenger.Lancamentos.Adapters.Repository.Query.ListarOperacoes
{
    using CodeChallenger.Lancamentos.Application.UseCases.ListarOperacoes;
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Lancamentos.Domain.Repository;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class ListarOperacoesQueryHandler : IRequestHandler<ListarOperacoesQueryCommand, IEnumerable<ListarOperacoesQueryResult>>
    {
        public ListarOperacoesQueryHandler(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        private readonly IReadRepository _readRepository;

        public async Task<IEnumerable<ListarOperacoesQueryResult>> Handle(ListarOperacoesQueryCommand request, CancellationToken cancellationToken)
        {
            return await (from r in _readRepository.GetQuery<Operacao>()
                          where (request.Movimento.HasValue || r.Movimento == request.Movimento)
                            && (request.ComParcelamento.HasValue || r.NumeroParcela > 1)
                            && (request.DataInicio.HasValue || r.DataRealizacao >= request.DataInicio)
                            && (request.DataTermino.HasValue || r.DataRealizacao <= request.DataTermino)
                            && (string.IsNullOrEmpty(request.Comentario)|| r.Comentario.Contains(request.Comentario))
                          select new ListarOperacoesQueryResult
                          {
                              Comentario = r.Comentario ?? null!,
                              DataCriacao = r.DataCriacao,
                              DataRealizacao = r.DataRealizacao,
                              Id = r.Id,
                              Identificador = r.Identificador,
                              Movimento = r.Movimento,
                              NumeroParcela = r.NumeroParcela,
                              TotalParcelas = r.TotalParcelas,
                              ValorParcela = r.ValorParcela,
                              ValorTotal = r.ValorTotal
                          }).ToListAsync();
        }
    }
}
