namespace CodeChallenger.Sdk.WebApi.Filters
{
    using Amx.Infrastructure.Auth.Abstractions;
    using Amx.Infrastructure.Http.Abstractions;
    using CodeChallenger.Sdk.WebApi.Attributes;
    using CodeChallenger.Sdk.WebApi.Authorization;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public class AuthorizationActionFilter : IAsyncAuthorizationFilter
    {
        public AuthorizationActionFilter(ITokenSession tokenSession,
            IServiceProvider serviceProvider,
            IAuthorizationResolver authorizationResolver = null)
        {
            _tokenSession = tokenSession;

            _authorizationResolver = authorizationResolver
                ?? serviceProvider.GetRequiredService<DefaultAuthorizationResolver>();
        }

        private readonly ITokenSession _tokenSession;
        private readonly IAuthorizationResolver _authorizationResolver;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if (!allowAnonymous)
            {
                if (_tokenSession.IsAuthenticated)
                {
                    var authAttribute = context.ActionDescriptor.EndpointMetadata.OfType<AuthorizationAttribute>().FirstOrDefault();

                    if (authAttribute != null)
                    {
                        var userIdentifier = _tokenSession.UserTokenInfo.Identifier;

                        var hasPermission = _authorizationResolver != null
                            && await _authorizationResolver.HasAtLeastOneAsync(userIdentifier, authAttribute.Permissions);

                        if (!hasPermission)
                        {
                            context!.Result = new ForbidResult();
                        }
                    }
                }
                else
                {
                    context!.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
