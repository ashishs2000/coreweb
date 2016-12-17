using System.ComponentModel.DataAnnotations;
using Neogov.Administration.Core.Commands;
using Neogov.Core.Common.Contracts.BaseContracts;
using Neogov.Core.Common.Contracts.Interfaces;
using Neogov.Core.Common.CustomAttributes;
using Neogov.Core.Common.Events;
using Neogov.Core.Common.Events.Employee;
using Neogov.Core.Common.ValueObjects;

namespace Neogov.Administration.Core.DomainModels
{
    public class Employee : BaseDomainModel<int>, IAggregateRoot
    {
        [Required]
        public string FirstName { get; private set; }

        [Required]
        public string LastName { get; private set; }

        [Required]
        public string EmployeeNumber { get; private set; }

        [Required]
        public int PositionId { get; private set; }

        public int? ManagerId { get; set; }

        [Required]
        [Email]
        public string Email { get; private set; }

        public Address Address { get; set; }

        public bool IsActive { get; set; }

        private Employee()
        {
        }

        internal Employee(EmployeeCommand employee)
        {
            ApplyAndValidateCommand(employee);
            DomainEvent.Raise(()=>new EmployeeCreated(()=> EntityId));
        }

        internal void Update(EmployeeCommand employee)
        {
            ApplyAndValidateCommand(employee);
        }

        private void ApplyAndValidateCommand(EmployeeCommand command)
        {
            DomainEvent.Raise(() => new EmployeeManagerChanged(() => EntityId, ManagerId, command.ManagerId), ManagerId != command.ManagerId);
            DomainEvent.Raise(() => new EmployeeDeactivated(() => EntityId), IsActive && !command.IsActive);
            DomainEvent.Raise(() => new EmployeePositionChanged(() => EntityId, PositionId,command.PositionId), PositionId != command.PositionId);

            FirstName = command.FirstName;
            LastName = command.LastName;
            EmployeeNumber = command.EmployeeNumber;
            Email = command.Email;
            PositionId = command.PositionId;
            ManagerId = command.ManagerId;
            IsActive = command.IsActive;

            Validate();
        }
    }


}