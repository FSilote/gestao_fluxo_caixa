namespace CodeChallenger.Saldo.Domain.Encryption
{
    public interface ISha512Service
    {
        string Hash(string text);
    }
}
