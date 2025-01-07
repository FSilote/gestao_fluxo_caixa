namespace CodeChallenger.Saldo.Adapters.Repository.Query.ListarOperacoes
{
    using CodeChallenger.Saldo.Application.UseCases.ListarOperacoes;
    using CodeChallenger.Saldo.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Repository;
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
                            && (string.IsNullOrEmpty(request.Descricao)|| r.Descricao.Contains(request.Descricao))
                          select new ListarOperacoesQueryResult
                          {
                              Comentario = r.Descricao ?? null!,
                              DataCriacao = r.DataCriacao,
                              DataPrevista = r.DataPrevista,
                              DataRealizacao = r.DataRealizacao,
                              Id = r.Id,
                              Identificador = r.Identificador,
                              Movimento = r.Movimento,
                              NumeroParcela = r.NumeroParcela,
                              Status = r.Status,
                              TotalParcelas = r.TotalParcelas,
                              ValorParcela = r.ValorParcela,
                              ValorTotal = r.ValorTotal
                          }).ToListAsync();
        }
    }
}
