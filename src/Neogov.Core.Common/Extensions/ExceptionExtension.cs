using System;

namespace Neogov.Core.Common.Extensions
{
    public static class ExceptionExtension
    {
        public static T FindInner<T>(this Exception ex)
            where T : Exception
        {
            while (ex.InnerException != null)
            {
                var inner = ex.InnerException as T;
                if (inner != null)
                    return inner;
                ex = ex.InnerException;
            }
            return null;
        }

        public static void PushInnerExeceptions(this Exception exception, Action<string> queue)
        {

        }
    }
}