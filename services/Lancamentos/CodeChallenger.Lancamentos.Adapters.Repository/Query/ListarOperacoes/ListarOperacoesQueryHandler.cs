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
                          where (request.Movimento.HasValue == false || r.Movimento == request.Movimento)
                            && (request.ComParcelamento.HasValue == false || r.NumeroParcela > 1)
                            && (request.DataInicio.HasValue == false || r.DataRealizacao >= request.DataInicio)
                            && (request.DataTermino.HasValue == false || r.DataRealizacao <= request.DataTermino)
                            && (string.IsNullOrEmpty(request.Descricao)|| r.Descricao.Contains(request.Descricao))
                          select new ListarOperacoesQueryResult
                          {
                              Categoria = r.Categoria,
                              Descricao = r.Descricao ?? null!,
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
