using System;
using Neogov.Core.Common.Enums;

namespace Neogov.Core.Common.Security
{
    public interface IUserContext
    {
        int EmployerId { get; }
        int UserId { get; }
        int EmployeeId { get; }

        string Name { get; }
        string Username { get; }

        TimeZoneInfo EmployerTimeZone { get; }
        ProfileType ProfileType { get; }

        bool HasAccess(SecurityObject @object, Crud requestedPermission, bool overrideProfile = false);
        bool CurrentUserHrUserOrAdmin();
        bool CurrentUserIsHrAdmin();
        bool CurrentUserIsHrUser();
        bool CurrentUserIsManager();
    }
}