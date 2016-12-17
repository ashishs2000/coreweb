using System;
using System.Collections.Concurrent;

namespace Neogov.Core.Common.Containers
{
    public class ContextContainer<T> where T : class
    {
        private readonly ConcurrentDictionary<string, T> _inner;

        public ContextContainer()
        {
            this._inner = new ConcurrentDictionary<string, T>();
        }

        public void AddContext(string guid, T collection)
        {
            this._inner.AddOrUpdate(guid, collection,
                (key, old) =>
                {
                    if (old != null)
                        throw new ApplicationException("The context is already in the container!");

                    return collection;
                });
        }

        public bool TryGetContext(string guid, out T collection)
        {
            T value;

            if (this._inner.TryGetValue(guid, out value))
            {
                if (value != null)
                {
                    collection = value;
                    return true;
                }
            }

            collection = null;
            return false;
        }
    }
}