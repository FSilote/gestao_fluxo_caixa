namespace CodeChallenger.Sdk.WebApi.Authorization
{
    using Amx.Infrastructure.Auth.Abstractions;
    using Amx.Infrastructure.Http.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DefaultAuthorizationResolver : IAuthorizationResolver
    {
        #region Ctrs

        public DefaultAuthorizationResolver(ITokenSession tokenSession)
        {
            this._tokenSession = tokenSession;
        }

        #endregion

        #region Attrs

        private readonly ITokenSession _tokenSession;

        #endregion

        public Task<bool> HasAtLeastOneAsync(IList<string> permissions)
        {
            var roles = this._tokenSession.UserTokenInfo.Roles;
            
            var hasPermission = roles.Count > 0
                && permissions?.Count > 0
                && permissions.Intersect(roles).Count() > 0;

            return Task.FromResult(hasPermission);
        }

        public Task<bool> HasAtLeastOneAsync(Guid userIdentifier, IList<string> permissions)
        {
            return this.HasAtLeastOneAsync(permissions);
        }

        public Task<bool> HasPermissionAsync(string permission)
        {
            var roles = this._tokenSession.UserTokenInfo.Roles;

            var hasPermission = roles.Count > 0
                && !string.IsNullOrEmpty(permission)
                && roles.Contains(permission);

            return Task.FromResult(hasPermission);
        }

        public Task<bool> HasPermissionAsync(Guid userIdentifier, string permission)
        {
            return this.HasPermissionAsync(userIdentifier, permission);
        }
    }
}
