using System;

namespace Neogov.Core.Common.Utilities
{
    public class SystemClock : Clock
    {
        public override DateTime Utc => DateTime.UtcNow;
    }
}