using SADC.Application.Dtos;
using SADC.Domain;
using SADC.Persistence.Models;

namespace SADC.Application.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> AddEmployees(EmployeeDto model);
        Task<EmployeeDto> UpdateEmployee(EmployeeDto model);

        Task<PageList<EmployeeDto>> GetAllEmployeesAsync(PageParams pageParams);
        Task<EmployeeDto> GetEmployeeByIdAsync(int employeeId);
    }
}
