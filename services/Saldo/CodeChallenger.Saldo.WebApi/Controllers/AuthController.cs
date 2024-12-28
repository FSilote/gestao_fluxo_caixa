using Asp.Versioning;
using CodeChallenger.Saldo.Application.UseCases.ExcluirOperacao;
using CodeChallenger.Saldo.Application.UseCases.ListarOperacoes;
using CodeChallenger.Saldo.Application.UseCases.Login;
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
