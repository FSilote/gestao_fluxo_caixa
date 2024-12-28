namespace CodeChallenger.Saldo.Domain.Auth
{
    using CodeChallenger.Saldo.Domain.Entity;

    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
    }
}
