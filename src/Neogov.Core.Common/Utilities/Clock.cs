using System;

namespace Neogov.Core.Common.Utilities
{
    public abstract class Clock : IClock
    {
        [ThreadStatic]
        private static IClock _current;

        public static IClock Current
        {
            get { return _current ?? (_current = new SystemClock()); }
            set { _current = value; }
        }

        public abstract DateTime Utc { get; }
    }

    public interface IClock
    {
        DateTime Utc { get; }
    }
}