namespace CodeChallenger.Lancamentos.WebApi.Controllers
{
    using Asp.Versioning;
    using CodeChallenger.Lancamentos.Application.UseCases.Login;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    //[Route("v{version:apiVersion}/[controller]")]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        public AuthController(ILogger<OperacaoController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        private readonly ILogger<OperacaoController> _logger;
        private readonly IMediator _mediator;

        [HttpPost("login")]
        public Task<LoginResult> RealizarLogin([FromBody] LoginCommand request)
        {
            return _mediator.Send(request);
        }
    }
}
