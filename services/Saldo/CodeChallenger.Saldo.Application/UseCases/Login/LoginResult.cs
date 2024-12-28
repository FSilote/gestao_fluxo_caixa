namespace CodeChallenger.Saldo.Application.UseCases.Login
{
    public class LoginResult
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome {  get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string AccessToken {  get; set; }
    }
}
