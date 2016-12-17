using System;

namespace Neogov.Core.Common.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowInReadOnlyModeAttribute : Attribute
    {
    }
}