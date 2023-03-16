using SADC.Domain;
using SADC.Persistence.Models;

namespace SADC.Persistence.Contracts
{
    public interface IEmployeePersist : IGeralPersist
    {
        Task<PageList<Employee>> GetAllEmployeesAsync(PageParams pageParams);
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task AddEmployeeFarm(int employeeId, List<int> farmId);
    }
}
