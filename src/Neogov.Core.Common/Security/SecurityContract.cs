using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Neogov.Core.Common.Enums;

namespace Neogov.Core.Common.Security
{
    public class SecurityContract
    {
        private readonly ReadOnlyDictionary<SecurityObject, Claim> _claims;
        private readonly ReadOnlyDictionary<SecurityObject, Claim> _employeeClaims;

        public SecurityContract(IEnumerable<Claim> claims, IEnumerable<Claim> employeeClaims, ProfileType profileType, bool isSuperAdmin = false)
        {
            ProfileType = profileType;
            this._claims = new ReadOnlyDictionary<SecurityObject, Claim>(claims.ToDictionary(p => p.SecurityObject, p => p));
            this._employeeClaims = new ReadOnlyDictionary<SecurityObject, Claim>(employeeClaims.ToDictionary(p => p.SecurityObject, p => p));
            IsSuperAdmin = isSuperAdmin;
        }

        public ReadOnlyDictionary<SecurityObject, Claim> GetClaims(bool overrideProfile = false)
        {
            return overrideProfile ? this._employeeClaims : this._claims;
        }

        public ProfileType ProfileType { get; }
        public bool IsSuperAdmin { get; private set; }
    }
}