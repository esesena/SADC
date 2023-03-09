using SADC.Domain;
using SADC.Persistence.Models;

namespace SADC.Persistence.Contracts
{
    public interface IEmployeePersist : IGeralPersist
    {
        Task<PageList<Employee>> GetAllEmployeesAsync(PageParams pageParams, bool includeFarms = false);
        Task<Employee> GetEmployeeByUserIdAsync(int userId, bool includeFarms = false);
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
    }
}
