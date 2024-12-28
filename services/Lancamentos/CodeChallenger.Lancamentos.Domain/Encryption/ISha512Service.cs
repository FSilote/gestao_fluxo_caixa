namespace CodeChallenger.Lancamentos.Domain.Encryption
{
    public interface ISha512Service
    {
        string Hash(string text);
    }
}
