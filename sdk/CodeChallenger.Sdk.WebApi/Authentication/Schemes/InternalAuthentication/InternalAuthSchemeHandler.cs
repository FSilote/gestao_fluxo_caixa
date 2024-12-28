namespace CodeChallenger.Sdk.WebApi.Authentication.Schemes.InternalAuthentication
{
    using Amx.Core.Domain.Entity.Authentication;
    using Amx.Infrastructure.Cryptography.Abstractions;
    using Amx.Infrastructure.Data.Abstractions.Repository;
    using Amx.Infrastructure.Http.Abstractions;
    using MediatR;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Security.Claims;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    public class InternalAuthSchemeHandler : AuthenticationHandler<InternalAuthSchemeOptions>
    {
        public InternalAuthSchemeHandler(IOptionsMonitor<InternalAuthSchemeOptions> options,
            ILoggerFactory loggerFactory,
            UrlEncoder encoder,
            ILogger<InternalAuthSchemeHandler> logger,
            ISha256Service sha256Service,
            ITokenSession tokenSession,
            IReadRepository readRepository = null)
            : base(options, loggerFactory, encoder)
        {
            _readRepository = readRepository;
            _sha256Service = sha256Service;
            _logger = logger;
            _tokenSession = tokenSession;
        }

        private readonly IReadRepository _readRepository;
        private readonly ISha256Service _sha256Service;
        private readonly ILogger<InternalAuthSchemeHandler> _logger;
        private readonly ITokenSession _tokenSession;

        private const string SERVICE_ID_HEADER_NAME = "x-amx-api-identifier";
        private const string CONTENT_HASH_HEADER_NAME = "x-amx-content-sha256";
        private const string TOKEN_DATE_HEADER_NAME = "x-amx-request-date";
        private const string USER_TOKEN_HEADER_NAME = "x-amx-request-user-token";
        private const int TOKEN_TIMEOUT = 15;

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var hmacToken = Request.Headers.Authorization.ToString();
            var serviceAccessId = Request.Headers[SERVICE_ID_HEADER_NAME].ToString();
            var content = Request.Headers[CONTENT_HASH_HEADER_NAME].ToString();
            var dateHeader = Request.Headers[TOKEN_DATE_HEADER_NAME].ToString();

            if (!string.IsNullOrEmpty(serviceAccessId)
                && !string.IsNullOrEmpty(content)
                && !string.IsNullOrEmpty(hmacToken)
                && !string.IsNullOrEmpty(dateHeader)
                && hmacToken.StartsWith("HMAC", StringComparison.InvariantCultureIgnoreCase))
            {
                var date = Convert.ToDateTime(dateHeader).ToUniversalTime();
                
                var service = _readRepository != null
                    ? await _readRepository.GetOneAsync<InternalAuthenticationCredential>(x => x.AccessId == serviceAccessId)
                    : null!;

                if (service == null)
                {
                    var message = $"Can not find the service from Service Identifier header ({serviceAccessId}).";
                    _logger.LogError(message);
                    return AuthenticateResult.Fail(message);
                }

                if (date.AddSeconds(TOKEN_TIMEOUT) < DateTime.UtcNow)
                {
                    var message = "Token date is expired.";
                    _logger.LogError(message);
                    return AuthenticateResult.Fail(message);
                }

                var signature = hmacToken.Split("Signature=").LastOrDefault();
                var signatureCheck = this.GetHmacSignature(service.AccessKey);

                if (!string.IsNullOrEmpty(signature)
                    && signature.Equals(signatureCheck, StringComparison.CurrentCultureIgnoreCase))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.System, $"Internal Auth: {Request.Path} | {service.ServiceName}"),
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
                            new Claim(ClaimTypes.Name, service.ServiceName),
                            new Claim(ClaimTypes.NameIdentifier, service.AccessId),
                        ]);
                    }

                    var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
            }

            return AuthenticateResult.Fail("Can not authenticate with informed credentials. Also check HTTP Request Headers.");
        }

        private string GetHmacSignature(string key)
        {
            var method = Request.Method.ToUpper();
            var path = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            var query = Request.QueryString;
            var date = Request.Headers[TOKEN_DATE_HEADER_NAME].ToString();
            var content = Request.Headers[CONTENT_HASH_HEADER_NAME].ToString();

            var pattern = $"{method};{path};{date};{content}";

            return _sha256Service.GenerateHmacSignature(pattern, key);
        }
    }
}
