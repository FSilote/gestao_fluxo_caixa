namespace CodeChallenger.Lancamentos.Application.Events.OperacaoDeletadaEvent
{
    using CodeChallenger.Lancamentos.Application.Events.Base;
    using System;

    public class OperacaoDeletadaEventCommand : BaseEventCommand
    {
        public int Id { get; set; }
        public Guid Identificador { get; set; }
    }
}
