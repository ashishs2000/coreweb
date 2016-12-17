using System;
using Neogov.Core.Common.Enums;

namespace Neogov.Core.Common.Security
{
    public class CurrentUser : IUserContext
    {
        public CurrentUser(int userId, int employeeId, int employerId, SecurityContract securityContract, string name = "", TimeZoneInfo employerTimeZone = null, string username = "")
        {
            SecurityContract = securityContract;
            EmployerId = employerId;
            EmployerTimeZone = employerTimeZone ?? TimeZoneInfo.Utc;
            EmployeeId = employeeId;
            UserId = userId;
            Name = name;
            Username = username;
        }

        public int UserId { get; }
        public int EmployeeId { get; }
        public int EmployerId { get; }

        public string Name { get; }
        public string Username { get; }

        public TimeZoneInfo EmployerTimeZone { get; }
        public SecurityContract SecurityContract { get; }

        public ProfileType ProfileType => SecurityContract.ProfileType;

        public bool CurrentUserHrUserOrAdmin()
        {
            return CurrentUserIsHrAdmin() || CurrentUserIsHrUser();
        }

        public bool CurrentUserIsHrAdmin()
        {
            return SecurityContract.ProfileType == ProfileType.HrAdmin;
        }

        public bool CurrentUserIsHrUser()
        {
            return SecurityContract.ProfileType == ProfileType.HrUser;
        }

        public bool CurrentUserIsManager()
        {
            return SecurityContract.ProfileType == ProfileType.Manager;
        }

        public bool HasAccess(SecurityObject @object, Crud requestedPermission, bool overrideProfile = false)
        {
            if (@object == SecurityObject.None)
                return true;

            var claims = this.SecurityContract.GetClaims(overrideProfile);

            Claim claim;
            if (!claims.TryGetValue(@object, out claim))
                return false;
            if (claim == null)
                return false;

            switch (requestedPermission)
            {
                case Crud.Read:
                    return claim.HasReadAccess;
                case Crud.Update:
                    return claim.HasUpdateAccess;
                case Crud.Insert:
                    return claim.HasAddAccess;
                case Crud.Delete:
                    return claim.HasDeleteAccess;
                default:
                    throw new ArgumentException($"Invalid {requestedPermission} argument");
            }
        }
    }
}