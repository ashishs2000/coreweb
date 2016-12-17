using System;
using Neogov.Core.Common.Enums;

namespace Neogov.Core.Common.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizationAttribute : Attribute
    {
        public SecurityObject SecurityObject { get; set; }
        public Crud OperationType { get; set; }
        public ProfileType[] ExcludedProfileTypes { get; set; }

        public AuthorizationAttribute(SecurityObject securityObject, Crud operationType, ProfileType[] excludedProfileTypes = null)
        {
            SecurityObject = securityObject;
            OperationType = operationType;
            ExcludedProfileTypes = excludedProfileTypes;
        }

        public AuthorizationAttribute(Crud operationType, ProfileType[] excludedProfileTypes = null)
            : this(SecurityObject.None, operationType, excludedProfileTypes)
        {
        }
    }
}