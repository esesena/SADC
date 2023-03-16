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

        public async Task<PageList<Planting>> GetAllPlantingsAsync(PageParams pageParams, bool includeFields = false)
        {
            IQueryable<Planting> query = _context.Plantings.Include(s => s.Seed)
                             .Include(f => f.Farm)
                             .Include(pf => pf.Fields)
                             .ThenInclude(f => f.Field);

                query = query.AsNoTracking()
                             .Where(p => (p.Harvest.ToLower().Contains(pageParams.Term.ToLower())) ||
                                          p.Seed.Description.ToLower().Contains(pageParams.Term.ToLower()) ||
                                          p.Fertilizing.ToLower().Contains(pageParams.Term.ToLower()))
                             .OrderBy(p => p.Id);

            return await PageList<Planting>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Planting> GetPlantingByIdAsync(int plantingId)
        {
            IQueryable<Planting> query = _context.Plantings
                                        .Include(s => s.Seed)
                                        .Include(f => f.Farm)
                                        .Include(pf => pf.Fields)
                                        .ThenInclude(f => f.Field);

            query = query.AsNoTracking().OrderBy(e => e.Id)
            .Where(e => e.Id == plantingId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task AddPlantingField(int plantingId, List<int> fieldIds)
        {
            foreach (var field in fieldIds)
            {
                PlantingField f = new PlantingField();
                f.PlantingId = plantingId;
                f.FieldId = field;
                _context.Add(f);
                await _context.SaveChangesAsync();
            }
        }
    }
}
