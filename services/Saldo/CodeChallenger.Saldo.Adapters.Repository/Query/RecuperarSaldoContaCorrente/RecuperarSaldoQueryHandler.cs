namespace CodeChallenger.Saldo.Adapters.Repository.Query.RecuperarSaldoContaCorrente
{
    using CodeChallenger.Lancamentos.Application.UseCases.RecuperarSaldoContaCorrente;
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Repository;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class RecuperarSaldoQueryHandler : IRequestHandler<RecuperarSaldoQueryCommand, RecuperarSaldoQueryResult>
    {
        public RecuperarSaldoQueryHandler(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        private readonly IReadRepository _readRepository;

        public async Task<RecuperarSaldoQueryResult> Handle(RecuperarSaldoQueryCommand request, CancellationToken cancellationToken)
        {
            var result = await (from r in _readRepository.GetQuery<ContaCorrente>()
                                select new RecuperarSaldoQueryResult
                                {
                                    Saldo = r.Saldo,
                                    DataAlteracao = r.DataAlteracao,
                                })
                                .FirstOrDefaultAsync(cancellationToken);

            return result ?? new RecuperarSaldoQueryResult();
        }
    }
}
