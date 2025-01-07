namespace CodeChallenger.Lancamentos.Application.Events.Base
{
    using MediatR;
    using System;

    public class BaseEventCommand : IRequest<bool>
    {
        public Guid EventId { get; set; }
        public DateTime EventDate { get; set; }
        public string? EventType { get; set; }
    }
}
