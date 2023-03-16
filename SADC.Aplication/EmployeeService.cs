using AutoMapper;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Domain;
using SADC.Persistence.Contracts;
using SADC.Persistence.Models;
using System.Collections.Generic;

namespace SADC.Application
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeePersist _employeePersist;
        private readonly IMapper _mapper;
        private readonly IGeralPersist _geralPersist;
        private readonly IFarmPersist _farmPersist;

        public EmployeeService(IEmployeePersist employeePersist, IMapper mapper, IGeralPersist geralPersist, IFarmPersist farmPersist)
        {
            _employeePersist = employeePersist;
            _mapper = mapper;
            _geralPersist = geralPersist;
            _farmPersist = farmPersist;
        }

        public async Task<EmployeeDto> AddEmployees(EmployeeDto model)
        {
            try
            {
                List<int> farmIds = new List<int>();
                foreach (var farm in model.FarmId)
                {
                    farmIds.Add(farm);
                }

                var employee = _mapper.Map<Employee>(model);

                _employeePersist.Add<Employee>(employee);

                if (await _employeePersist.SaveChangesAsync())
                {   
                    var employeeReturn = _mapper.Map<EmployeeDto>(employee);
                    await _employeePersist.AddEmployeeFarm(employee.Id, farmIds);
                    return employeeReturn;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeDto> UpdateEmployee(EmployeeDto model)
        {
            try
            {
                var employee = await _employeePersist.GetEmployeeByIdAsync(model.Id);
                if (employee == null) return null;

                model.Id = employee.Id;

                _mapper.Map(model, employee);

                _geralPersist.Update<Employee>(employee);

                if (await _employeePersist.SaveChangesAsync())
                {
                    var employeeRetorno = await _employeePersist.GetEmployeeByIdAsync(model.Id);

                    return _mapper.Map<EmployeeDto>(employeeRetorno);
                }

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<EmployeeDto>> GetAllEmployeesAsync(PageParams pageParams)
        {
            try
            {
                var employees = await _employeePersist.GetAllEmployeesAsync(pageParams);
                if (employees == null) return null;

                var resultado = _mapper.Map<PageList<EmployeeDto>>(employees);

                resultado.CurrentPage = employees.CurrentPage;
                resultado.TotalPages = employees.TotalPages;
                resultado.PageSize = employees.PageSize;
                resultado.TotalCount = employees.TotalCount;
                

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeDto> GetEmployeeByUserIdAsync(int employeeId)
        {
            try
            {
                var employee = await _employeePersist.GetEmployeeByIdAsync(employeeId);
                if (employee == null) return null;

                var resultado = _mapper.Map<EmployeeDto>(employee);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                var employee = await _employeePersist.GetEmployeeByIdAsync(employeeId);
                if (employee == null) return null;

                var resultado = _mapper.Map<EmployeeDto>(employee);
                resultado.FarmId = employee.Farms.Select(f => f.FarmId).ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
