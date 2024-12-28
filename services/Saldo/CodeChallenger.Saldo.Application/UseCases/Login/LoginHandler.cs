namespace CodeChallenger.Saldo.Application.UseCases.Login
{
    using CodeChallenger.Saldo.Domain.Auth;
    using CodeChallenger.Saldo.Domain.Encryption;
    using CodeChallenger.Saldo.Domain.Entity;
    using CodeChallenger.Saldo.Domain.Exceptions;
    using CodeChallenger.Saldo.Domain.Repository;
    using MediatR;
    using Serilog;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        public LoginHandler(ILogger logger, IReadRepository repository, 
            ITokenService tokenService, ISha512Service sha512Service)
        {
            _repository = repository;
            _logger = logger;
            _tokenService = tokenService;
            _sha512Service = sha512Service;
        }

        private readonly IReadRepository _repository;
        private readonly ILogger _logger;
        private readonly ITokenService _tokenService;
        private readonly ISha512Service _sha512Service;

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var senha = _sha512Service.Hash(request.Senha);
            var user = await _repository.GetOneAsync<Usuario>(x => x.Email == request.Login);

            if (user is null || !senha.Equals(user?.Senha))
            {
                throw new DomainException("Usuário ou senha incorretos.");
            }

            var accessToken = _tokenService.CreateToken(user);

            return new LoginResult
            {
                AccessToken = accessToken,
                Email = user.Email!,
                Id = user.Id,
                Nome = user.Nome!,
                Roles = user.Roles
            };
        }
    }
}
