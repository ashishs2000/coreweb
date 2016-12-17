namespace Neogov.Core.Common.Events.Dispatchers
{
    public class NullEventDispatcher : IDomainEventDispatcher
    {
        public static readonly IDomainEventDispatcher Instance = new NullEventDispatcher();

        private NullEventDispatcher()
        {
        }

        public void Dispatch<TEvent>(TEvent @event)
        {
        }
    }
}