﻿namespace CodeChallenger.Lancamentos.Application.UseCases.RealizarOperacao
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Lancamentos.Domain.Events;
    using CodeChallenger.Lancamentos.Domain.Messaging;
    using CodeChallenger.Lancamentos.Domain.Repository;
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
                .SetCategoria(request.Categoria)
                .SetDescricao(request.Descricao)
                .SetDataPrevista(request.Data)
                .SetDataRealizacao(request.Data)
                .SetMovimento(request.GetMovimentoOperacao())
                .SetValorTotal(request.Valor)
                .SetValorParcela(request.Valor)
                .SetStatus(request.Data.Date <= DateTime.UtcNow.Date 
                    ? StatusOperacao.EFETIVADO 
                    : StatusOperacao.PREVISTO);

            await _writeRepository.SaveOrUpdateAsync<Operacao>(operacao);

            await _publisher.Publish(TopicNames.OPERACOES, new OperacaoRegistradaEvent
            {
                Id = operacao.Id,
                Categoria = request.Categoria,
                Descricao = operacao.Descricao,
                TotalParcelas = operacao.TotalParcelas,
                DataCriacao = operacao.DataCriacao,
                DataPrevista = operacao.DataPrevista,
                DataRealizacao = operacao.DataRealizacao,
                Identificador = operacao.Identificador,
                Movimento = operacao.Movimento,
                NumeroParcela = operacao.NumeroParcela,
                ValorParcela = operacao.ValorParcela,
                ValorTotal = operacao.ValorTotal,
                Status = operacao.Status
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
