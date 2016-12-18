using System.Data.Entity;
using Neogov.Core.Common.Contracts.Interfaces;

namespace Neogov.Core.Common.Infrastructure.DatabaseTransaction
{
    public class ExistingTransaction : ITransaction
    {
        private readonly Database _database;
        private bool _commited;

        public ExistingTransaction(Database database)
        {
            this._database = database;
            this._commited = false;
        }

        public void Dispose()
        {
            if (!_commited)
                this._database.CurrentTransaction?.Rollback();
        }

        public void Commit()
        {
            this._commited = true;
        }
    }
}