namespace CodeChallenger.Saldo.Adapters.Repository.Query.RecuperarOperacao
{
    using CodeChallenger.Saldo.Application.UseCases.RecuperarOperacao;
    using CodeChallenger.Saldo.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Repository;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class RecuperarOperacaoQueryHandler : IRequestHandler<RecuperarOperacaoQueryCommand, RecuperarOperacaoQueryResult?>
    {
        public RecuperarOperacaoQueryHandler(IReadRepository readRepository)
        {
            this._readRepository = readRepository;
        }

        private readonly IReadRepository _readRepository;

        public Task<RecuperarOperacaoQueryResult?> Handle(RecuperarOperacaoQueryCommand request, CancellationToken cancellationToken)
        {
            return (from r in _readRepository.GetQuery<Operacao>()
                    where r.Id == request.Id
                    select new RecuperarOperacaoQueryResult
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
                    }).FirstOrDefaultAsync();
        }
    }
}
