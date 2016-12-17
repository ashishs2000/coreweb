namespace Neogov.Core.Common.Events
{
    public interface IDomainEventHandler<in TEvent>
    {
        void Handle(TEvent @event);
    }

    public interface IDomainEventDispatcher
    {
        void Dispatch<TEvent>(TEvent @event);
    }
}