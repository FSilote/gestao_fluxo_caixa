namespace CodeChallenger.Saldo.Application.UseCases.RealizarOperacao
{
    using CodeChallenger.Saldo.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Events;
    using CodeChallenger.Saldo.Domain.Messaging;
    using CodeChallenger.Saldo.Domain.Repository;
    using MediatR;
    using Serilog;
    using System.Threading.Tasks;

    public class RealizarOperacaoHandler : IRequestHandler<RealizarOperacaoCommand, RealizarOperacaoResult>
    {
        public RealizarOperacaoHandler(ILogger logger, IReadRepository repository,
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

        public async Task<RealizarOperacaoResult> Handle(RealizarOperacaoCommand request, CancellationToken cancellationToken)
        {
            var operacao = new Operacao()
                .SetComentario(request.Comentario)
                .SetDataRealizacao(request.Data)
                .SetMovimento(request.GetMovimentoOperacao())
                .SetValorTotal(request.Valor);

            await _writeRepository.SaveOrUpdateAsync<Operacao>(operacao);

            await _publisher.Publish(TopicNames.OPERACOES, new OperacaoRegistradaEvent
            {
                Id = operacao.Id,
                Comentario = operacao.Comentario,
                TotalParcelas = operacao.TotalParcelas,
                DataCriacao = operacao.DataCriacao,
                DataRealizacao = operacao.DataRealizacao,
                Identificador = operacao.Identificador,
                Movimento = operacao.Movimento,
                NumeroParcela = operacao.NumeroParcela,
                ValorParcela = operacao.ValorParcela,
                ValorTotal = operacao.ValorTotal                
            });

            return new RealizarOperacaoResult
            {
                DataRealizacao = operacao.DataRealizacao,
                Id = operacao.Id,
                Identificador = operacao.Identificador,
                Valor = operacao.ValorTotal
            };
        }
    }
}
