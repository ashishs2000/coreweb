using System;
using Neogov.Core.Common.Enums;

namespace Neogov.Core.Common.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Property)]
    public class WithSecurityObjectAttribute : Attribute
    {
        public SecurityObject SecurityObject { get; set; }

        public WithSecurityObjectAttribute(SecurityObject securityObject)
        {
            SecurityObject = securityObject;
        }
    }
}