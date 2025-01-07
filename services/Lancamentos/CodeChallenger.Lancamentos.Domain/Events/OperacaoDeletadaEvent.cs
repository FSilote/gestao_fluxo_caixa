namespace CodeChallenger.Lancamentos.Domain.Events
{
    using CodeChallenger.Lancamentos.Domain.Events.Base;

    public class OperacaoDeletadaEvent: AbstractBaseEvent
    {
        public int Id { get; set; }
        public Guid Identificador { get; set; }
    }
}
