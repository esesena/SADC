using AutoMapper;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Domain;
using SADC.Persistence.Contracts;
using SADC.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Application
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeePersist _employeePersist;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeePersist employeePersist, IMapper mapper)
        {
            _employeePersist = employeePersist;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> AddEmployees(int userId, EmployeeAddDto model)
        {
            try
            {
                var employee = _mapper.Map<Employee>(model);
                employee.UserId = userId;

                _employeePersist.Add<Employee>(employee);

                if (await _employeePersist.SaveChangesAsync())
                {
                    var employeeRetorno = await _employeePersist.GetEmployeeByUserIdAsync(userId);

                    return _mapper.Map<EmployeeDto>(employeeRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeDto> UpdateEmployee(int userId, EmployeeUpdateDto model)
        {
            try
            {
                var employee = await _employeePersist.GetEmployeeByUserIdAsync(userId, false);
                if (employee == null) return null;

                model.Id = employee.Id;
                model.UserId = employee.UserId;

                _mapper.Map(model, employee);

                _employeePersist.Update<Employee>(employee);

                if (await _employeePersist.SaveChangesAsync())
                {
                    var employeeRetorno = await _employeePersist.GetEmployeeByUserIdAsync(userId, false);

                    return _mapper.Map<EmployeeDto>(employeeRetorno);
                }

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<EmployeeDto>> GetAllEmployeesAsync(PageParams pageParams, bool includeFarms = false)
        {
            try
            {
                var employee = await _employeePersist.GetAllEmployeesAsync(pageParams, includeFarms);
                if (employee == null) return null;

                var resultado = _mapper.Map<PageList<EmployeeDto>>(employee);

                resultado.CurrentPage = employee.CurrentPage;
                resultado.TotalPages = employee.TotalPages;
                resultado.PageSize = employee.PageSize;
                resultado.TotalCount = employee.TotalCount;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeDto> GetEmployeeByUserIdAsync(int userId, bool includeFarms = false)
        {
            try
            {
                var employee = await _employeePersist.GetEmployeeByUserIdAsync(userId, includeFarms);
                if (employee == null) return null;

                var resultado = _mapper.Map<EmployeeDto>(employee);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
