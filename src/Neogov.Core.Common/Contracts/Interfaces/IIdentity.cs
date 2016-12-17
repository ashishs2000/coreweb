namespace Neogov.Core.Common.Contracts.Interfaces
{
    public interface IIdentity<out T>
    {
        T EntityId { get; }
    }
}