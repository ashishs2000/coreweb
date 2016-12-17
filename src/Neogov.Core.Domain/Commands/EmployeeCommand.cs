namespace Neogov.Administration.Core.Commands
{
    public class EmployeeCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Email { get; set; }

        public int PositionId { get; set; }
        public int? ManagerId { get; set; }
        public bool IsActive { get; set; }
    }
}