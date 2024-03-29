﻿using SADC.Domain;
using SADC.Persistence.Context;
using SADC.Persistence.Contracts;
using SADC.Persistence.Models;
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

        public async Task<PageList<Farm>> GetAllFarmsAsync(PageParams pageParams)
        {
            IQueryable<Farm> query = _context.Farms
                            .Include(c => c.Fields);

            query = query.AsNoTracking()
                         .Where(e => (e.Name.ToLower().Contains(pageParams.Term.ToLower()) ||
                                      e.Location.ToLower().Contains(pageParams.Term.ToLower())))
                         .OrderBy(e => e.Id);

            return await PageList<Farm>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Farm> GetFarmByIdAsync(int farmId)
        {
            IQueryable<Farm> query = _context.Farms
                            .Include(c => c.Fields);

            query = query.AsNoTracking().OrderBy(e => e.Id)
            .Where(e => e.Id == farmId);

            return await query.FirstOrDefaultAsync();
        }

    }
}
