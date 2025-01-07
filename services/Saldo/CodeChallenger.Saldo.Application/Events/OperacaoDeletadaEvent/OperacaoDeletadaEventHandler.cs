namespace CodeChallenger.Lancamentos.Application.Events.OperacaoDeletadaEvent
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Repository;
    using MediatR;
    using Serilog;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class OperacaoDeletadaEventHandler : IRequestHandler<OperacaoDeletadaEventCommand, bool>
    {
        public OperacaoDeletadaEventHandler(IReadRepository readRepository,
            IWriteRepository writeRepository, ILogger logger)
        {
            _logger = logger;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;
        private readonly ILogger _logger;

        public async Task<bool> Handle(OperacaoDeletadaEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var operacao = await _readRepository.GetOneAsync<Operacao>(x => x.IdIntegracao == request.Id);

                if (operacao is not null)
                {
                    await _writeRepository.DeleteAsync(operacao);

                    var contaCorrente = await _readRepository.GetOneAsync<ContaCorrente>(x => x.Id > 0)
                        ?? new ContaCorrente();

                    if (operacao.Status == StatusOperacao.EFETIVADO)
                    {
                        if (operacao.Movimento == Movimento.ENTRADA)
                        {
                            _ = contaCorrente.Debitar(operacao.ValorParcela);
                        }
                        else
                        {
                            _ = contaCorrente.Creditar(operacao.ValorParcela);
                        }

                        await _writeRepository.SaveOrUpdateAsync(contaCorrente);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro ao processar mensagem do tipo {GetType().Name} ({request.EventId}).", ex);
                return false;
            }
        }
    }
}
