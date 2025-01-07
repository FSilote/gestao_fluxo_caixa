namespace CodeChallenger.Lancamentos.Application.UseCases.RealizarOperacaoParcelada
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Lancamentos.Domain.Events;
    using CodeChallenger.Lancamentos.Domain.Messaging;
    using CodeChallenger.Lancamentos.Domain.Repository;
    using MediatR;
    using Serilog;
    using System.Collections.Generic;
    using System.Net.Http.Headers;
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
            var parcelas = Operacao.DefinirOperacesParceladas(request.Valor, request.GetMovimentoOperacao(), 
                request.DataPrimeiraParcela, request.TotalParcelas, request.Descricao, request.Categoria);

            var events = new List<OperacaoRegistradaEvent>();
            var result = new List<RealizarOperacaoParceladaResult>();

            foreach (var parcela in parcelas)
            {
                await _writeRepository.SaveOrUpdateAsync(parcela);
                
                events.Add(new OperacaoRegistradaEvent
                {
                    Id = parcela.Id,
                    Categoria = parcela.Categoria,
                    Descricao = parcela.Descricao,
                    TotalParcelas = parcela.TotalParcelas,
                    DataCriacao = parcela.DataCriacao,
                    DataRealizacao = parcela.DataRealizacao,
                    DataPrevista = parcela.DataPrevista,
                    Identificador = parcela.Identificador,
                    Movimento = parcela.Movimento,
                    NumeroParcela = parcela.NumeroParcela,
                    ValorParcela = parcela.ValorParcela,
                    ValorTotal = parcela.ValorTotal,
                    Status = parcela.Status,
                });

                result.Add(new RealizarOperacaoParceladaResult
                {
                    DataPrevista = parcela.DataPrevista,
                    DataRealizacao = parcela.DataRealizacao,
                    Id = parcela.Id,
                    Identificador = parcela.Identificador,
                    NumeroParcela = parcela.NumeroParcela,
                    TotalParcelas = parcela.TotalParcelas,
                    ValorParcela = parcela.ValorParcela,
                    ValorTotal = parcela.ValorTotal
                });
            }

            foreach(var ev in events)
            {
                await _publisher.Publish(TopicNames.OPERACOES, ev);
            }

            return result;
        }
    }
}
