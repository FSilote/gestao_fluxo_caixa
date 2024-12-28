namespace CodeChallenger.Lancamentos.Domain.Auth
{
    using CodeChallenger.Lancamentos.Domain.Entity;

    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
    }
}
