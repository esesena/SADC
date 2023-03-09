using SADC.API.Extensions;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SADC.API.Helpers;

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
        private readonly IUtil _util;
        private readonly string _destiny = "EmployeeImage";


        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment hostEnviroment, IAccountService accountService, IUtil util)
        {
            _employeeService = employeeService;
            _hostEnviroment = hostEnviroment;
            _accountService = accountService;
            _util = util;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
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

        [HttpPost("upload-image/{employeeId}")]
        public async Task<IActionResult> UploadImage(int employeeId)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
                if (employee == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _util.DeleteImage(employee.ImageURL, _destiny);
                    employee.ImageURL = await _util.SaveImage(file, _destiny);
                }
                var employeeRetorno = await _employeeService.UpdateEmployee(employeeId, employee);

                return Ok(employeeRetorno);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar Farms. Erro: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(EmployeeDto model)
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
