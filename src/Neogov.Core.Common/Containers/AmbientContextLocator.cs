using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Neogov.Core.Common.Containers
{
    public class AmbientContextLocator
    {
        private readonly string _scopeTypeName;
        public AmbientContextLocator(string scopeTypeName)
        {
            this._scopeTypeName = scopeTypeName;
        }

        public string LocateId()
        {
            var ids = CallContext.LogicalGetData(_scopeTypeName) as Stack<string>;
            return ids != null && ids.Count > 0 ? ids.Peek() : null;
        }

        public string CreateId()
        {
            var ids = CallContext.LogicalGetData(_scopeTypeName) as Stack<string>;
            if (ids == null)
            {
                ids = new Stack<string>();
                CallContext.LogicalSetData(_scopeTypeName, ids);
            }

            var id = Guid.NewGuid().ToString();
            ids.Push(id);
            return id;
        }

        public void ClearId(string id)
        {
            var ids = CallContext.LogicalGetData(_scopeTypeName) as Stack<string>;
            if (ids == null || ids.Count == 0 || ids.Peek() != id)
                throw new ApplicationException("Cannot clear unavailable scope identification!");
            ids.Pop();
        }
    }
}