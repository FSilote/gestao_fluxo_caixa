namespace CodeChallenger.Lancamentos.Application.Events.ParcelaPagaEvent
{
    using CodeChallenger.Lancamentos.Application.Events.Base;
    using CodeChallenger.Lancamentos.Domain.Entity;
    using System;

    public class ParcelaPagaEventCommand : BaseEventCommand
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime? DataRealizacao { get; set; }
        public StatusOperacao Status { get; set; }
    }
}
