namespace CodeChallenger.Sdk.WebApi.Authentication.Schemes.BasicAuthentication
{
    using Amx.Core.Domain.Entity.Authentication;
    using Amx.Infrastructure.Cryptography.Abstractions;
    using Amx.Infrastructure.Data.Abstractions.Repository;
    using Amx.Infrastructure.Http.Abstractions;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    public class BasicAuthSchemeHandler : AuthenticationHandler<BasicAuthSchemeOptions>
    {
        public BasicAuthSchemeHandler(IOptionsMonitor<BasicAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISha512Service sha512Service,
            ITokenSession tokenSession,
            IReadRepository readRepository = null)
            : base(options, logger, encoder)
        {
            _readRepository = readRepository;
            _sha512Service = sha512Service;
            _tokenSession = tokenSession;
        }

        private readonly IReadRepository _readRepository;
        private readonly ISha512Service _sha512Service;
        private readonly ITokenSession _tokenSession;
        private const string USER_TOKEN_HEADER_NAME = "x-amx-request-user-token";

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers.Authorization;

            if (!string.IsNullOrEmpty(token) && token.ToString().StartsWith("Basic", StringComparison.InvariantCultureIgnoreCase))
            {
                var encodedCredentials = token.ToString().Substring("Basic ".Length).Trim();
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials)).Split(':');

                var username = credentials[0];
                var password = credentials[1];

                var hash = _sha512Service.Hash(password);

                var user = _readRepository != null
                    ? await _readRepository.GetOneAsync<BasicAuthenticationCredential>(x => x.Username == username)
                    : null!;

                if (!string.IsNullOrEmpty(user?.Password)
                    && user.Password.Equals(hash, StringComparison.InvariantCultureIgnoreCase))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.System, $"Basic Auth: {Request.Path} | {user.Username}"),
                        new Claim(ClaimTypes.AuthenticationMethod, "AMX_MULTI_AUTH_SCHEME")
                    };

                    if (Request.Headers.TryGetValue(USER_TOKEN_HEADER_NAME, out var requestUserToken)
                        && !string.IsNullOrEmpty(requestUserToken))
                    {
                        var jwtClaims = _tokenSession.GetClaimsFromJwtToken(requestUserToken!);
                        claims.AddRange(jwtClaims);
                    }
                    else
                    {
                        claims.AddRange([
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.NameIdentifier, user.Username)
                        ]);
                    }

                    var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
            }

            return AuthenticateResult.Fail("Can not authenticate with informed credentials.");
        }
    }
}
