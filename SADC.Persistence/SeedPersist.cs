using Microsoft.EntityFrameworkCore;
using SADC.Domain;
using SADC.Persistence.Context;
using SADC.Persistence.Contracts;
using SADC.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Persistence
{
    public class SeedPersist : ISeedPersist
    {
        private readonly SADCContext _context;

        public SeedPersist(SADCContext context)
        {
            _context = context;
        }

        public async Task<PageList<Seed>> GetAllSeedsAsync(PageParams pageParams)
        {
            IQueryable<Seed> query = _context.Seeds;

            query = query.AsNoTracking()
            .OrderBy(e => e.Id);

            return await PageList<Seed>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Seed> GetSeedByIdAsync(int seedId)
        {
            IQueryable<Seed> query = _context.Seeds;

            query = query.AsNoTracking().OrderBy(e => e.Id)
            .Where(e => e.Id == seedId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Seed> GetSeedForSelectAsync()
        {
            IQueryable<Seed> query = _context.Seeds;

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
