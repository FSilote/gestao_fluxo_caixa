namespace CodeChallenger.Saldo.Adapters.Repository.Query.RecuperarRelatorioLancamentosDiario
{
    using CodeChallenger.Lancamentos.Application.UseCases.RecuperarRelatorioLancamentosDiario;
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Repository;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class RecuperarRelatorioLancamentosDiarioQueryHandler
        : IRequestHandler<RecuperarRelatorioLancamentosDiarioQueryCommand, RecuperarRelatorioLancamentosDiarioQueryResult>
    {
        public RecuperarRelatorioLancamentosDiarioQueryHandler(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        private readonly IReadRepository _readRepository;

        public async Task<RecuperarRelatorioLancamentosDiarioQueryResult> Handle(
            RecuperarRelatorioLancamentosDiarioQueryCommand request,
            CancellationToken cancellationToken)
        {
            var operacoes = await (from r in _readRepository.GetQuery<Operacao>()
                                   where r.DataRealizacao != null
                                      && r.DataRealizacao.Value.Date == request.Data.Date
                                   select new RecuperarRelatorioLancamentosDiarioQueryResultItem
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
                                   })
                                   .ToListAsync();

            var totalEntradas = operacoes
                .Where(x => x.Movimento == Movimento.ENTRADA && x.Status == StatusOperacao.EFETIVADO)
                .Sum(x => x.ValorParcela);

            var totalSaidas = operacoes
                .Where(x => x.Movimento == Movimento.SAIDA && x.Status == StatusOperacao.EFETIVADO)
                .Sum(x => x.ValorParcela);

            return new RecuperarRelatorioLancamentosDiarioQueryResult
            {
                TotalEntradas = totalEntradas,
                TotalSaidas = totalSaidas,
                SaldoDia = decimal.Subtract(totalEntradas, totalSaidas),
                Operacoes = operacoes
            };
        }
    }
}
