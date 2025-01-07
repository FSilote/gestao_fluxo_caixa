namespace CodeChallenger.Lancamentos.Application.Events.ParcelaPagaEvent
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Repository;
    using MediatR;
    using Serilog;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ParcelaPagaEventHandler : IRequestHandler<ParcelaPagaEventCommand, bool>
    {
        public ParcelaPagaEventHandler(IReadRepository readRepository,
            IWriteRepository writeRepository, ILogger logger)
        {
            _logger = logger;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;
        private readonly ILogger _logger;

        public async Task<bool> Handle(ParcelaPagaEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var operacao = await _readRepository.GetOneAsync<Operacao>(x => x.IdIntegracao == request.Id);

                if (operacao is not null && (!operacao.DataAlteracao.HasValue || operacao!.DataAlteracao < request.EventDate))
                {
                    _ = operacao
                        .SetDataAlteracao(request.EventDate)
                        .SetDataPrevista(request.DataPrevista)
                        .SetDataRealizacao(request.DataRealizacao)
                        .SetIdIntegracao(request.Id)
                        .SetStatus(request.Status);

                    await _writeRepository.SaveOrUpdateAsync(operacao);

                    var contaCorrente = await _readRepository.GetOneAsync<ContaCorrente>(x => x.Id > 0)
                        ?? new ContaCorrente();

                    if (operacao.Status == StatusOperacao.EFETIVADO)
                    {
                        if (operacao.Movimento == Movimento.ENTRADA)
                        {
                            _ = contaCorrente.Creditar(operacao.ValorParcela);
                        }
                        else
                        {
                            _ = contaCorrente.Debitar(operacao.ValorParcela);
                        }

                        await _writeRepository.SaveOrUpdateAsync(contaCorrente);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro ao processar mensagem do tipo {GetType().Name} ({request.EventId}).", ex);
                return false;
            }
        }
    }
}
