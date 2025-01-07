namespace CodeChallenger.Saldo.WebApi.Controllers
{
    using Asp.Versioning;
    using CodeChallenger.Lancamentos.Application.UseCases.RecuperarRelatorioLancamentosDiario;
    using CodeChallenger.Saldo.Application.UseCases.ListarOperacoes;
    using CodeChallenger.Saldo.Application.UseCases.RecuperarOperacao;
    using CodeChallenger.Saldo.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Exceptions;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
        public async Task<RecuperarOperacaoQueryResult> RecuperarOperacaoPorId([FromRoute] int id)
        {
            return await _mediator.Send(new RecuperarOperacaoQueryCommand(id))
                ?? throw new NotFoundException("Operacao não encontrada."); ;
        }
    }
}
