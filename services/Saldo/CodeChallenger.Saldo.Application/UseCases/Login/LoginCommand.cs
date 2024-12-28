namespace CodeChallenger.Saldo.Application.UseCases.Login
{
    using MediatR;

    public class LoginCommand : IRequest<LoginResult>
    {
        public LoginCommand(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public string Login { get; protected set; }
        public string Senha { get; private set; }
    }
}
