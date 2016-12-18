using System;
using Neogov.Core.Common.Containers;
using Neogov.Core.Common.Events.Context;
using Neogov.Core.Common.Events.Dispatchers;

namespace Neogov.Core.Common.Events
{
    public static class DomainEventManager
    {
        private const string DomainEventContextName = "DomainEventContext";

        private static readonly ContextContainer<DomainEventContext> ContextContainer = new ContextContainer<DomainEventContext>();
        private static readonly AmbientContextLocator AmbientContextLocator = new AmbientContextLocator(DomainEventContextName);

        public static DomainEventContext CreateSuppressingContext()
        {
            return CreateContext(NullEventDispatcher.Instance);
        }

        public static DomainEventContext CreateContext(IDomainEventDispatcher dispatcher)
        {
            var id = AmbientContextLocator.CreateId();

            var context = new DomainEventContext(
                externalCleanupAction: () => AmbientContextLocator.ClearId(id),
                dispatcher: dispatcher);

            ContextContainer.AddContext(id, context);

            return context;
        }

        public static DomainEventContext Context
        {
            get
            {
                var id = AmbientContextLocator.LocateId();
                if (id != null)
                {
                    DomainEventContext context;
                    if (ContextContainer.TryGetContext(id, out context))
                        return context;
                }
                return null;
            }
        }

        public static void Raise<TEvent>(Func<TEvent> @eventSource, bool shouldRaise = true)
        {
            if (Context != null)
            {
                if(!shouldRaise)
                    return;

                Context.Add(eventSink =>
                {
                    var @event = eventSource();

                    eventSink.Dispatch(@event);
                });
            }
            else
            {
                throw new ApplicationException("Raise can only work inside valid DomainEventContext!");
            }
        }

        public static void Raise<TEvent, TDomainModel>(this TDomainModel self, Func<TEvent> @eventSource)
        {
            Raise(eventSource);
        }
    }
}