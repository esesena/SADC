using SADC.Domain;
using SADC.Persistence.Context;
using SADC.Persistence.Contracts;
using SADC.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Persistence
{
    public class PlantingPersist : GeralPersist, IPlantingPersist
    {
        private readonly SADCContext _context;

        public PlantingPersist(SADCContext context) : base(context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<PageList<Planting>> GetAllPlantingsAsync(PageParams pageParams, bool includePlots = false)
        {
            IQueryable<Planting> query = _context.Plantings;

            if (includePlots)
            {
                query = query.Include(p => p.Seed)
                    .Include(p => p.PlantingPlot)
                    .ThenInclude(pe => pe.Plot)
                    .Where(p => (p.Harvest.ToLower().Contains(pageParams.Term.ToLower())) || 
                                 p.Seed.Description.ToLower().Contains(pageParams.Term.ToLower()) || 
                                 p.Fertilizing.ToLower().Contains(pageParams.Term.ToLower()));
            }

            return await PageList<Planting>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Planting> GetPlantingByIdAsync(int plantingId)
        {
            IQueryable<Planting> query = _context.Plantings
                                        .Include(c => c.Seed)
                                        .Include(p => p.PlantingPlot)
                                        .ThenInclude(p => p.Plot);

            query = query.AsNoTracking().OrderBy(e => e.Id)
            .Where(e => e.Id == plantingId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
