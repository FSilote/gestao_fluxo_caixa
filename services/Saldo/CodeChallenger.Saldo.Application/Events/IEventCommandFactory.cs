namespace CodeChallenger.Lancamentos.Application.Events
{
    using CodeChallenger.Lancamentos.Application.Events.Base;

    public interface IEventCommandFactory
    {
        BaseEventCommand Create(string message);
    }
}
