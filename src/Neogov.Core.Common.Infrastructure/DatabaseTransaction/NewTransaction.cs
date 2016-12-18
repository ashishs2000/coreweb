using System;
using System.Data;
using System.Data.Entity;
using Neogov.Core.Common.Contracts.Interfaces;

namespace Neogov.Core.Common.Infrastructure.DatabaseTransaction
{
    public class NewTransaction : ITransaction
    {
        private readonly Database _database;
        private DbContextTransaction _trans;

        public NewTransaction(IsolationLevel level, Database database)
        {
            this._database = database;
            this._trans = this._database.BeginTransaction(level);
        }

        public virtual void Commit()
        {
            if (this._database.CurrentTransaction == null)
                throw new NotSupportedException("Transaction was already commited or disposed!");
            this._trans.Commit();
            this._trans = null;
        }

        public virtual void Dispose()
        {
            if (this._trans != null &&
                this._database.CurrentTransaction != null)
                this._trans.Rollback();
        }
    }
}