namespace CodeChallenger.Saldo.WebApi.Encryption
{
    using CodeChallenger.Saldo.Domain.Encryption;
    using System.Security.Cryptography;
    using System.Text;

    public class Sha512Service : ISha512Service
    {
        public string Hash(string text)
        {
            var byteValue = Encoding.UTF8.GetBytes(text);
            var byteHash = SHA512.HashData(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}
