using System;

namespace Neogov.Core.Common.Events.Employee
{
    public class EmployeeCreated : IDomainEvent
    {
        public Func<int> GetEmployeeId { get; }
        public EmployeeCreated(Func<int> getEmployeeId)
        {
            GetEmployeeId = getEmployeeId;
        }
    }

    public class EmployeeDeactivated : EmployeeCreated
    {
        public EmployeeDeactivated(Func<int> getEmployeeId) : base(getEmployeeId)
        {
        }
    }

    public class EmployeeManagerChanged : EmployeeCreated
    {
        public int? OldManager { get; set; }
        public int? NewManager { get; set; }

        public EmployeeManagerChanged(Func<int> getEmployeeId, int? oldManager, int? newManager) : base(getEmployeeId)
        {
            OldManager = oldManager;
            NewManager = newManager;
        }
    }

    public class EmployeePositionChanged : EmployeeCreated
    {
        public int OldPosition { get; set; }
        public int NewPosition { get; set; }

        public EmployeePositionChanged(Func<int> getEmployeeId, int oldPosition, int newPosition) : base(getEmployeeId)
        {
            OldPosition = oldPosition;
            NewPosition = newPosition;
        }
    }
}