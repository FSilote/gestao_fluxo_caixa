namespace CodeChallenger.Lancamentos.Domain.Events
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Lancamentos.Domain.Events.Base;

    public class ParcelaPagaEvent : AbstractBaseEvent
    {
        public int Id { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime? DataRealizacao { get; set; }
        public StatusOperacao Status { get; set; }
    }
}
