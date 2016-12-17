using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace Neogov.Core.Common.Events.Dispatchers
{
    public class AutofacEventDispatcher : IDomainEventDispatcher
    {
        private readonly ILifetimeScope container;

        public AutofacEventDispatcher(ILifetimeScope container)
        {
            this.container = container;
        }

        public void Dispatch<TEvent>(TEvent @event)
        {
            IEnumerable<IDomainEventHandler<TEvent>> services;
            if (container.TryResolve(out services) &&
                services != null)
            {
                services = services.Distinct();
                foreach (var service in services)
                    service.Handle(@event);
            }
        }
    }
}