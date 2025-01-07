namespace CodeChallenger.Lancamentos.Application.UseCases.RealizarPagamentoParcela
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Lancamentos.Domain.Events;
    using CodeChallenger.Lancamentos.Domain.Exceptions;
    using CodeChallenger.Lancamentos.Domain.Messaging;
    using CodeChallenger.Lancamentos.Domain.Repository;
    using MediatR;
    using Serilog;
    using System.Threading.Tasks;

    public class RealizarPagamentoParcelaHandler : IRequestHandler<RealizarPagamentoParcelaCommand, RealizarPagamentoParcelaResult>
    {
        public RealizarPagamentoParcelaHandler(ILogger logger, IReadRepository repository,
            IWriteRepository writeRepository, IPublisherService publisherService)
        {
            _repository = repository;
            _writeRepository = writeRepository;
            _logger = logger;
            _publisher = publisherService;
        }

        private readonly IWriteRepository _writeRepository;
        private readonly IReadRepository _repository;
        private readonly ILogger _logger;
        private readonly IPublisherService _publisher;

        public async Task<RealizarPagamentoParcelaResult> Handle(RealizarPagamentoParcelaCommand request, CancellationToken cancellationToken)
        {
            var operacao = await _repository.GetByIdAsync<Operacao>(request.IdParcela);

            _ = operacao
                .SetDataRealizacao(DateTime.UtcNow)
                .SetStatus(StatusOperacao.EFETIVADO);

            if (operacao is null)
            {
                throw new NotFoundException("Operacao nao encontrada.");
            }

            if (operacao.Status == StatusOperacao.EFETIVADO)
            {
                throw new DomainException("Esta operacao ja foi paga anteriormente.");
            }

            await _writeRepository.SaveOrUpdateAsync<Operacao>(operacao);

            await _publisher.Publish(TopicNames.OPERACOES, new ParcelaPagaEvent
            {
                Id = operacao.Id,
                Status = operacao.Status,
                DataPrevista = operacao.DataPrevista,
                DataRealizacao = operacao.DataRealizacao
            });

            return new RealizarPagamentoParcelaResult
            {
                Id = operacao.Id,
                Status = operacao.Status
            };
        }
    }
}
