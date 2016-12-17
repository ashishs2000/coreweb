using Neogov.Core.Common.Interfaces;

namespace Neogov.Core.Common.Contracts.Interfaces
{
    public interface IBaseDomain<out T> : IIdentity<T>, IBelongToEmployer, IUserModifiableEntity
    {

    }
}