using Neogov.Administration.Core.DomainModels;

namespace Neogov.Administration.Core.InfrastructureContracts
{
    public interface IEmployeeRepository
    {
        Employee Get(int employeeId, int employerId);
        void Save(Employee employee);
    }

    
}