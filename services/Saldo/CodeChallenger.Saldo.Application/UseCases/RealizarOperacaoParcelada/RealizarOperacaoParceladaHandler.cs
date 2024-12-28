namespace CodeChallenger.Saldo.Application.UseCases.RealizarOperacaoParcelada
{
    using CodeChallenger.Saldo.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Events;
    using CodeChallenger.Saldo.Domain.Messaging;
    using CodeChallenger.Saldo.Domain.Repository;
    using MediatR;
    using Serilog;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RealizarOperacaoParceladaHandler : IRequestHandler<RealizarOperacaoParceladaCommand, IEnumerable<RealizarOperacaoParceladaResult>>
    {
        public RealizarOperacaoParceladaHandler(ILogger logger, IReadRepository repository, 
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

        public async Task<IEnumerable<RealizarOperacaoParceladaResult>> Handle(RealizarOperacaoParceladaCommand request, CancellationToken cancellationToken)
        {
            var parcelas = Operacao.DefinirOperacesParceladas(request.Valor, request.Movimento, 
                request.DataPrimeiraParcela, request.TotalParcelas, request.Comentario);

            var events = new List<OperacaoRegistradaEvent>();

            foreach (var parcela in parcelas)
            {
                await _writeRepository.SaveOrUpdateAsync(parcela);
                
                events.Add(new OperacaoRegistradaEvent
                {
                    Id = parcela.Id,
                    Comentario = parcela.Comentario,
                    TotalParcelas = parcela.TotalParcelas,
                    DataCriacao = parcela.DataCriacao,
                    DataRealizacao = parcela.DataRealizacao,
                    Identificador = parcela.Identificador,
                    Movimento = parcela.Movimento,
                    NumeroParcela = parcela.NumeroParcela,
                    ValorParcela = parcela.ValorParcela,
                    ValorTotal = parcela.ValorTotal
                });
            }

            foreach(var ev in events)
            {
                await _publisher.Publish(TopicNames.OPERACOES, ev);
            }

            return parcelas.Select(x => new RealizarOperacaoParceladaResult
            {
                DataRealizacao = x.DataRealizacao,
                Id = x.Id,
                Identificador = x.Identificador,
                NumeroParcela = x.NumeroParcela,
                TotalParcelas = x.TotalParcelas,
                ValorParcela = x.ValorParcela,
                ValorTotal = x.ValorTotal
            });
        }
    }
}
