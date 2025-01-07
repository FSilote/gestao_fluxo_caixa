namespace CodeChallenger.Saldo.WebApi.Controllers
{
    using Asp.Versioning;
    using CodeChallenger.Lancamentos.Application.UseCases.RecuperarRelatorioLancamentosDiario;
    using CodeChallenger.Lancamentos.Application.UseCases.RecuperarSaldoContaCorrente;
    using CodeChallenger.Saldo.Domain.Entity;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    //[Route("v{version:apiVersion}/[controller]")]
    [Route("[controller]")]
    public class RelatorioController : ControllerBase
    {
        public RelatorioController(ILogger<RelatorioController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        private readonly ILogger<RelatorioController> _logger;
        private readonly IMediator _mediator;

        [HttpGet("lancamentos/diario")]
        [Authorize(Policy = Roles.GERENTE)]
        public Task<RecuperarRelatorioLancamentosDiarioQueryResult> RecuperarRelatorioLancamentosDiario(
            [FromQuery] RecuperarRelatorioLancamentosDiarioQueryCommand request)
        {
            return _mediator.Send(request);
        }
    }
}
