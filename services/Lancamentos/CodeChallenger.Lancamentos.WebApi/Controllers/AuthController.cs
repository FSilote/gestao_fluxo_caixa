using Asp.Versioning;
using CodeChallenger.Lancamentos.Application.UseCases.ExcluirOperacao;
using CodeChallenger.Lancamentos.Application.UseCases.ListarOperacoes;
using CodeChallenger.Lancamentos.Application.UseCases.Login;
using CodeChallenger.Lancamentos.Application.UseCases.RealizarOperacao;
using CodeChallenger.Lancamentos.Application.UseCases.RealizarOperacaoParcelada;
using CodeChallenger.Lancamentos.Application.UseCases.RecuperarOperacao;
using CodeChallenger.Lancamentos.Domain.Entity;
using CodeChallenger.Lancamentos.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenger.Lancamentos.WebApi.Controllers
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
