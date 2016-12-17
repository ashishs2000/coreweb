using Neogov.Administration.Core.Commands;
using Neogov.Administration.Core.DomainModels;
using Neogov.Administration.Core.InfrastructureContracts;
using Neogov.Core.Common.Contracts.BaseContracts;
using Neogov.Core.Common.Wrappers;

namespace Neogov.Administration.Core.ApplicationService
{
    public interface IEmployeeService
    {
        Result<int> Add(EmployeeCommand command);
        Result<int> Update(int employeeId, EmployeeCommand command);
    }

    public class EmployeeService : ApplicationServiceBase, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Result<int> Add(EmployeeCommand command)
        {
            var employee = new Employee(command);
            _employeeRepository.Save(employee);

            return Result.Ok(employee.EntityId);
        }

        public Result<int> Update(int employeeId, EmployeeCommand command)
        {
            var employee = _employeeRepository.Get(employeeId, EmployerId);
            if(employee == null)
                return Result.Fail(employeeId,"Employee not found");

            employee.Update(command);
            _employeeRepository.Save(employee);


            return Result.Ok(employeeId,$"Employee {employee.FirstName} {employee.LastName} created successfully");
        }
    }
}