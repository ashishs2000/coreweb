using System;

namespace Neogov.Core.Common.Interfaces
{
    public interface IUserModifiableEntity
    {
        int CreatedByUserId { get; }
        DateTime CreatedOn { get; }
        int UpdatedByUserId { get; }
        DateTime UpdatedOn { get; }

        void SetCreatedBy(int createdByUserId);
        void SetUpdatedBy(int updatedByUserId);
    }
}