using System;
using System.Linq;
using Neogov.Core.Common.Contracts.Interfaces;
using Neogov.Core.Common.Exceptions;
using Neogov.Core.Common.Extensions;
using Neogov.Core.Common.Utilities;

namespace Neogov.Core.Common.Contracts.BaseContracts
{
    public abstract class BaseDomainModel<T> : IBaseDomain<T>
    {
        public T EntityId { get; protected set; }
        public int EmployerId { get; protected set; }

        public int CreatedByUserId { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public int UpdatedByUserId { get; protected set; }
        public DateTime UpdatedOn { get; protected set; }

        public Guid ObjectId { get; set; }

        protected BaseDomainModel()
        {
            ObjectId = Guid.Empty;
        }

        public void SetEmployerID(int employerId)
        {
            EmployerId = employerId;
        }

        public void SetCreatedBy(int userId)
        {
            CreatedByUserId = userId;
            CreatedOn = Clock.Current.Utc;
            UpdatedByUserId = userId;
            UpdatedOn = Clock.Current.Utc;
        }

        public void SetUpdatedBy(int userId)
        {
            UpdatedByUserId = userId;
            UpdatedOn = Clock.Current.Utc;
        }

        protected virtual void Validate()
        {
            var errors = this.ValidateProperties();
            if (errors.Any())
                throw new CoreException(errors.ToArray());
        }
    }
}