namespace CodeChallenger.Saldo.Domain.Entity
{
    public class Usuario : AbstractBaseEntity
    {
        public virtual string? Nome { get; protected set; }
        public virtual string? Email { get; protected set; }
        public virtual string? Senha { get; protected set; }
        public virtual string? PerfisAcesso {  get; protected set; }
        
        public virtual IEnumerable<string> Roles { get => PerfisAcesso?.Split(";") ?? []; }

        public virtual Usuario SetNome(string nome)
        {
            this.Nome = nome;
            return this;
        }

        public virtual Usuario SetEmail(string email)
        {
            this.Email = email;
            return this;
        }

        public virtual Usuario SetSenha(string senha)
        {
            this.Senha = senha;
            return this;
        }

        public virtual Usuario SetPerfisAcesso(IList<string> perfis)
        {
            this.PerfisAcesso = string.Join(";", perfis?.Where(x => !string.IsNullOrEmpty(x)).Select(x => x) ?? []);
            return this;
        }
    }
}
