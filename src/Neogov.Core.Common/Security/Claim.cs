using Neogov.Core.Common.Enums;

namespace Neogov.Core.Common.Security
{
    public class Claim
    {
        public Claim(SecurityObject securityObject, bool hasReadAccess, bool hasAddAccess, bool hasUpdateAccess, bool hasDeleteAccess)
        {
            HasDeleteAccess = hasDeleteAccess;
            HasUpdateAccess = hasUpdateAccess;
            HasAddAccess = hasAddAccess;
            HasReadAccess = hasReadAccess;
            SecurityObject = securityObject;
        }

        public SecurityObject SecurityObject { get; private set; }
        public bool HasReadAccess { get; private set; }
        public bool HasAddAccess { get; private set; }
        public bool HasUpdateAccess { get; private set; }
        public bool HasDeleteAccess { get; private set; }

        public override string ToString()
        {
            var rule = "";
            if (HasAddAccess)
                rule += "C";
            if (HasReadAccess)
                rule += "R";
            if (HasUpdateAccess)
                rule += "U";
            if (HasDeleteAccess)
                rule += "D";
            return SecurityObject.ToString() + " " + rule;
        }
    }
}