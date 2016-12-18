using System;
using System.Data.Entity;
using System.Linq;

namespace Neogov.Core.Common.Infrastructure.DatabaseContext
{
    public class ReadonlyContext<TContext> : CoreDbContext<TContext>
        where TContext : DbContext
    {
        public ReadonlyContext(string connectionStringOrName)
            : base(connectionStringOrName)
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        internal TEntity AddNew<TEntity>() where TEntity : class
        {
            var item = base.Set<TEntity>().Create();
            base.Set<TEntity>().Add(item);
            return item;
        }

        internal TEntity Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            return base.Set<TEntity>().Add(entity);
        }

        public new virtual IQueryable<TEntity> Set<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new virtual IQueryable Set(Type entityType)
        {
            return base.Set(entityType);
        }
    }
}