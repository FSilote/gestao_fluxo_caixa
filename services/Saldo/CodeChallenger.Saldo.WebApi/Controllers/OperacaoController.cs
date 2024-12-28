using Asp.Versioning;
using CodeChallenger.Saldo.Application.UseCases.ExcluirOperacao;
using CodeChallenger.Saldo.Application.UseCases.ListarOperacoes;
using CodeChallenger.Saldo.Application.UseCases.RealizarOperacao;
using CodeChallenger.Saldo.Application.UseCases.RealizarOperacaoParcelada;
using CodeChallenger.Saldo.Application.UseCases.RecuperarOperacao;
using CodeChallenger.Saldo.Domain.Entity;
using CodeChallenger.Saldo.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenger.Saldo.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    //[Route("v{version:apiVersion}/[controller]")]
    [Route("[controller]")]
    public class OperacaoController : ControllerBase
    {
        public OperacaoController(ILogger<OperacaoController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        private readonly ILogger<OperacaoController> _logger;
        private readonly IMediator _mediator;

        [HttpGet]
        public Task<IEnumerable<ListarOperacoesQueryResult>> ListarOperacoes([FromQuery] ListarOperacoesQueryCommand request)
        {
            return _mediator.Send(request);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Roles.ATENDENTE)]
        public async Task<RecuperarOperacaoQueryResult> RecuperarOperacaoPorId([FromRoute] int id)
        {
            return await _mediator.Send(new RecuperarOperacaoQueryCommand(id))
                ?? throw new NotFoundException("Operacao não encontrada."); ;
        }

        [HttpPost("entrada")]
        [Authorize(Policy = Roles.ATENDENTE)]
        public async Task<RealizarOperacaoResult> RealizarOperacaoEntrada([FromBody] RealizarOperacaoCommand request)
        {
            _ = request
                .SetMovimentoOperacao(Domain.Entity.Movimento.ENTRADA);

            var result = await _mediator.Send(request);

            return result;
        }

        [HttpPost("entrada/parcelada")]
        [Authorize(Policy = Roles.ATENDENTE)]
        public async Task<IEnumerable<RealizarOperacaoParceladaResult>> RealizarOperacaoEntradaParcelada(
            [FromBody] RealizarOperacaoParceladaCommand request)
        {
            _ = request
                .SetMovimentoOperacao(Domain.Entity.Movimento.ENTRADA);

            var result = await _mediator.Send(request);

            return result;
        }

        [HttpPost("saida")]
        [Authorize(Policy = Roles.ATENDENTE)]
        public async Task<RealizarOperacaoResult> RealizarOperacaoSaida([FromBody] RealizarOperacaoCommand request)
        {
            _ = request
                .SetMovimentoOperacao(Domain.Entity.Movimento.SAIDA);

            var result = await _mediator.Send(request);

            return result;
        }

        [HttpPost("saida/parcelada")]
        [Authorize(Policy = Roles.ATENDENTE)]
        public async Task<IEnumerable<RealizarOperacaoParceladaResult>> RealizarOperacaoSaidaParcelada(
            [FromBody] RealizarOperacaoParceladaCommand request)
        {
            _ = request
                .SetMovimentoOperacao(Domain.Entity.Movimento.SAIDA);

            var result = await _mediator.Send(request);

            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Roles.GERENTE)]
        public async Task<ExcluirOperacaoResult> ExcluirOperacao([FromRoute] int id)
        {
            var result = await _mediator.Send(new ExcluirOperacaoCommand(id));

            return result.Sucesso
                ? result
                : throw new NotFoundException("Operação não encontrada.");
        }
    }
}
