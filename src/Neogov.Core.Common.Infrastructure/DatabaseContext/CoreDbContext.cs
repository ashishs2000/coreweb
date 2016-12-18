using System.Data;
using System.Data.Entity;
using System.Linq;
using Neogov.Core.Common.Contracts.Interfaces;
using Neogov.Core.Common.Infrastructure.DatabaseTransaction;
using Neogov.Core.Common.Interfaces;
using Neogov.Core.Common.Security;

namespace Neogov.Core.Common.Infrastructure.DatabaseContext
{
    public abstract class CoreDbContext<TContext> : DbContext where TContext : DbContext
    {
        static CoreDbContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        protected internal CoreDbContext(string connectionStringOrName = "CoreData")
            : base(connectionStringOrName)
        {
        }

        public ITransaction WithNewOrExistingTransaction(IsolationLevel level = IsolationLevel.ReadUncommitted)
        {
            if (Database.CurrentTransaction != null)
                return new ExistingTransaction(this.Database);
            else
                return new NewTransaction(level, this.Database);
        }

        public override int SaveChanges()
        {
            SetEmployerId();
            SetCreateUpdateInfo();
            return base.SaveChanges();
        }

        private void SetCreateUpdateInfo()
        {
            foreach (var entry in ChangeTracker.Entries<IUserModifiableEntity>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.SetCreatedBy(UserContext.Current.UserId);

                entry.Entity.SetUpdatedBy(UserContext.Current.UserId);
            }
        }

        private void SetEmployerId()
        {
            foreach (var entry in ChangeTracker
                .Entries<IBelongToEmployer>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Entity.SetEmployerID(UserContext.Current.EmployerId);
            }
        }
    }
}