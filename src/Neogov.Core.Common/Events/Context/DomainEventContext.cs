using System;
using System.Collections.Generic;
using System.Threading;

namespace Neogov.Core.Common.Events.Context
{
    public class DomainEventContext : IDisposable
    {
        private readonly Action _externalCleanupAction;
        private readonly IDomainEventDispatcher _dispatcher;
        private ThreadLocal<List<Action<IDomainEventDispatcher>>> _callbacks;

        internal DomainEventContext(
            Action externalCleanupAction,
            IDomainEventDispatcher dispatcher)
        {
            this._callbacks = CreateStorage();

            this._externalCleanupAction = externalCleanupAction;
            this._dispatcher = dispatcher;
        }

        private ThreadLocal<List<Action<IDomainEventDispatcher>>> CreateStorage()
        {
            return new ThreadLocal<List<Action<IDomainEventDispatcher>>>(trackAllValues: true);
        }

        public void Add(Action<IDomainEventDispatcher> handlerCallback)
        {
            if (!this._callbacks.IsValueCreated)
                this._callbacks.Value = new List<Action<IDomainEventDispatcher>>();
            this._callbacks.Value.Add(handlerCallback);
        }

        public void Dispatch()
        {
            foreach (var actions in this._callbacks.Values)
            {
                foreach (var action in actions)
                    action(this._dispatcher);
                actions.Clear();
            }

            this._callbacks.Dispose();
            this._callbacks = CreateStorage();
        }

        public void Dispose()
        {
            _externalCleanupAction();
        }
    }
}