namespace CodeChallenger.Lancamentos.Application.Events.RealizarOperacao
{
    using CodeChallenger.Saldo.Domain.Repository;
    using MediatR;
    using Serilog;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class OperacaoRegistradaEventHandler : IRequestHandler<OperacaoRegistradaEventCommand, bool>
    {
        public OperacaoRegistradaEventHandler(IReadRepository readRepository,
            IWriteRepository writeRepository, ILogger logger)
        {
            _logger = logger;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;
        private readonly ILogger _logger;

        public async Task<bool> Handle(OperacaoRegistradaEventCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(10000, cancellationToken);

            _logger.Information($"Id {request.EventId} -> {request.EventType}: {request.EventDate:dd/MM/yyyy HH:mm:ss}");

            // return Task.FromResult(true);
            return true;
        }
    }
}
