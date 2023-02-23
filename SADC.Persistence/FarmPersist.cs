using SADC.Domain;
using SADC.Domain.Identity;
using SADC.Persistence.Context;
using SADC.Persistence.Contracts;
using SADC.Persistence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SADC.Persistence
{
    public class FarmPersist : IFarmPersist
    {
        private readonly SADCContext _context;

        public FarmPersist(SADCContext context)
        {
            _context = context;
        }

        public async Task<PageList<Farm>> GetAllFarmsAsync(int userId, PageParams pageParams)
        {
            IQueryable<Farm> query = _context.Farms
                            .Include(c => c.Plots);

            query = query.AsNoTracking()
                         .Where(e => (e.Name.ToLower().Contains(pageParams.Term.ToLower())) &&
                         e.UserId == userId)
            .OrderBy(e => e.Id);

            return await PageList<Farm>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Farm> GetFarmByIdAsync(int userId, int farmId)
        {
            IQueryable<Farm> query = _context.Farms
                            .Include(c => c.Plots);

            query = query.AsNoTracking().OrderBy(e => e.Id)
            .Where(e => e.Id == farmId && e.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
