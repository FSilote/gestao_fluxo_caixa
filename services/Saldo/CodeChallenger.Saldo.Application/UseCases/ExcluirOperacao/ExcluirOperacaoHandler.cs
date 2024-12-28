namespace CodeChallenger.Saldo.Application.UseCases.ExcluirOperacao
{
    using CodeChallenger.Saldo.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Events;
    using CodeChallenger.Saldo.Domain.Messaging;
    using CodeChallenger.Saldo.Domain.Repository;
    using MediatR;
    using Serilog;
    using System.Threading.Tasks;

    public class ExcluirOperacaoHandler : IRequestHandler<ExcluirOperacaoCommand, ExcluirOperacaoResult>
    {
        public ExcluirOperacaoHandler(ILogger logger, IReadRepository repository,
            IWriteRepository writeRepository, IPublisherService publisher)
        {
            _repository = repository;
            _writeRepository = writeRepository;
            _logger = logger;
            _publisher = publisher;
        }

        private readonly IWriteRepository _writeRepository;
        private readonly IReadRepository _repository;
        private readonly ILogger _logger;
        private readonly IPublisherService _publisher;

        public async Task<ExcluirOperacaoResult> Handle(ExcluirOperacaoCommand request, CancellationToken cancellationToken)
        {
            var operacao = await _repository.GetByIdAsync<Operacao>(request.Id);

            var sucesso = operacao is not null
                && await _writeRepository.DeleteAsync(operacao);

            if (operacao is not null && sucesso)
            {
                await _publisher.Publish(TopicNames.OPERACOES, new OperacaoDeletadaEvent
                {
                    Id = request.Id,
                    Identificador = operacao.Identificador
                });
            }

            return new ExcluirOperacaoResult
            {
                Id = request.Id,
                Sucesso = sucesso
            };
        }
    }
}
