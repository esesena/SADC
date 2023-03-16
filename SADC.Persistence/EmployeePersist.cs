using SADC.Domain;
using SADC.Domain.Identity;
using SADC.Persistence.Context;
using SADC.Persistence.Contracts;
using SADC.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace SADC.Persistence
{
    public class EmployeePersist : GeralPersist, IEmployeePersist
    {
        private readonly SADCContext _context;

        public EmployeePersist(SADCContext context) : base(context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<PageList<Employee>> GetAllEmployeesAsync(PageParams pageParams)
        {
            IQueryable<Employee> query = _context.Employees
                             .Include(ef => ef.Farms)
                             .ThenInclude(f => f.Farm)
                             .ThenInclude(f => f.Fields);

            query = query.AsNoTracking()
                         .Where(p => (p.FirstName.ToLower().Contains(pageParams.Term.ToLower()) ||
                                      p.LastName.ToLower().Contains(pageParams.Term.ToLower())) ||
                                      p.Function == Domain.Enums.Function.Director)
                         .OrderBy(p => p.Id);

            return await PageList<Employee>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            IQueryable<Employee> query = _context.Employees
                                                 .Include(ef => ef.Farms)
                                                 .ThenInclude(f => f.Farm);

            query = query.AsNoTracking().OrderBy(e => e.Id)
            .Where(e => e.Id == employeeId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task AddEmployeeFarm(int employeeId, List<int> farmIds)
        {
            foreach (var farm in farmIds)
            {
                EmployeesFarms f = new EmployeesFarms();
                f.EmployeeId = employeeId;
                f.FarmId = farm;
                _context.Add(f);
                await _context.SaveChangesAsync();
            }
        }
    }
}
