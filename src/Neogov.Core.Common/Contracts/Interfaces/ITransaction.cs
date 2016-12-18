using System;

namespace Neogov.Core.Common.Contracts.Interfaces
{
    public interface ITransaction : IDisposable
    {
        void Commit();
    }
}