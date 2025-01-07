namespace CodeChallenger.Saldo.WebApi.Controllers
{
    using Asp.Versioning;
    using CodeChallenger.Lancamentos.Application.UseCases.RecuperarSaldoContaCorrente;
    using CodeChallenger.Saldo.Domain.Entity;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    //[Route("v{version:apiVersion}/[controller]")]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        public ContaCorrenteController(ILogger<ContaCorrenteController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        private readonly ILogger<ContaCorrenteController> _logger;
        private readonly IMediator _mediator;

        [HttpGet("saldo")]
        [Authorize(Policy = Roles.GERENTE)]
        public Task<RecuperarSaldoQueryResult> RecuperarSaldoContaCorrente()
        {
            return _mediator.Send(new RecuperarSaldoQueryCommand());
        }
    }
}
