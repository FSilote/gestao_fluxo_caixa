namespace CodeChallenger.Saldo.WebApi.Auth
{
    using CodeChallenger.Saldo.Domain.Auth;
    using CodeChallenger.Saldo.Domain.Entity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class TokenService : ITokenService
    {
        public TokenService(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfigurationRoot _configuration;

        public string CreateToken(Usuario usuario)
        {
            var handler = new JwtSecurityTokenHandler();

            var privateKey = Encoding.UTF8.GetBytes(_configuration["authentication:jwtKey"] 
                ?? throw new Exception("JWT Token key not found."));

            var credentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = GenerateClaims(usuario)
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(Usuario usuario)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("id", usuario.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, usuario.Email!));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Nome!));
            ci.AddClaim(new Claim(ClaimTypes.Email, usuario.Email!));

            foreach (var role in usuario.Roles ?? [])
                ci.AddClaim(new Claim(ClaimTypes.Role, role));

            return ci;
        }
    }
}
