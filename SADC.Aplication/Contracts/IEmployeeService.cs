using SADC.Application.Dtos;
using SADC.Persistence.Models;

namespace SADC.Application.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> AddEmployees(int userId, EmployeeAddDto model);
        Task<EmployeeDto> UpdateEmployee(int userId, EmployeeUpdateDto model);

        Task<PageList<EmployeeDto>> GetAllEmployeesAsync(PageParams pageParams, bool includeFarms = false);
        Task<EmployeeDto> GetEmployeeByUserIdAsync(int userId, bool includeFarms = false);
    }
}
