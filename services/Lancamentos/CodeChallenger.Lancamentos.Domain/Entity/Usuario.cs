namespace CodeChallenger.Lancamentos.Domain.Entity
{
    public class Usuario : AbstractBaseEntity
    {
        public virtual string? Nome { get; protected set; }
        public virtual string? Email { get; protected set; }
        public virtual string? Senha { get; protected set; }
        public virtual string? PerfisAcesso {  get; protected set; }
        
        public virtual IEnumerable<string> Roles { get => PerfisAcesso?.Split(";") ?? []; }
    }
}
