namespace CodeChallenger.Lancamentos.Domain.Events.Base
{
    using System;

    public abstract class AbstractBaseEvent
    {
        public Guid EventId => Guid.NewGuid();
        public DateTime EventDate => DateTime.UtcNow;
        public string EventType => this.GetType().Name;
    }
}
