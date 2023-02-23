using SADC.API.Extensions;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SADC.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IAccountService _accountService;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment hostEnviroment, IAccountService accountService)
        {
            _employeeService = employeeService;
            _hostEnviroment = hostEnviroment;
            _accountService = accountService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams)
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync(pageParams, true);
                if (employees == null) return NoContent();

                Response.AddPagination(employees.CurrentPage, employees.PageSize, employees.TotalCount, employees.TotalPages);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar employees. Erro: {ex.Message}");
            }
        }
        [HttpGet()]
        public async Task<IActionResult> GetByEmployees()
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByUserIdAsync(User.GetUserId(), true);
                if (employee == null) return NoContent();

                return Ok(employee);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Employees. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeAddDto model)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByUserIdAsync(User.GetUserId(), false);
                if (employee == null)
                    employee = await _employeeService.AddEmployees(User.GetUserId(), model);

                return Ok(employee);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar Employees. Erro: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(EmployeeUpdateDto model)
        {
            try
            {
                var employee = await _employeeService.UpdateEmployee(User.GetUserId(), model);
                if (employee == null) return NoContent();

                return Ok(employee);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar Employees. Erro: {ex.Message}");
            }
        }
    }
}
