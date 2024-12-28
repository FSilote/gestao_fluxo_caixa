namespace CodeChallenger.Sdk.WebApi.Http
{
    using Amx.Infrastructure.Http.Abstractions;
    using Amx.Infrastructure.Http.Abstractions.Models;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    public class TokenSession : ITokenSession
    {
        public bool IsAuthenticated => UserTokenInfo != null && UserTokenInfo.Identifier != default;

        public UserTokenInfo UserTokenInfo { get; private set; }
        public string JwtToken { get; private set; }
        public string IpAddress { get; private set; }

        public void SetUserTokenInfo(UserTokenInfo userTokenInfo)
        {
            UserTokenInfo = userTokenInfo;
        }

        public void SetJwtToken(string token)
        {
            JwtToken = token;
        }

        public void SetIpAddress(string ipAddress)
        {
            IpAddress = ipAddress;
        }

        public IEnumerable<Claim> GetClaimsFromJwtToken() => this.GetClaimsFromJwtToken(this.JwtToken);

        public IEnumerable<Claim> GetClaimsFromJwtToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    token = token.Replace("Bearer ", string.Empty);

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                    return securityToken.Claims;
                }
                catch
                {
                }
            }

            return [];
        }
    }
}
