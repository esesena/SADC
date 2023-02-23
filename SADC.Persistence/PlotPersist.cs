using SADC.Domain;
using SADC.Persistence.Context;
using SADC.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Persistence
{
    public class PlotPersist : IPlotPersist
    {
        private readonly SADCContext _context;

        public PlotPersist(SADCContext context)
        {
            _context = context;
        }

        public async Task<Plot> GetPlotByIdsAsync(int plotId, int farmId)
        {
            IQueryable<Plot> query = _context.Plots;
            query = query.AsNoTracking()
                         .Where(plot => plot.FarmId == farmId
                                     && plot.Id == plotId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Plot[]> GetPlotsByFarmIdAsync(int farmId)
        {
            IQueryable<Plot> query = _context.Plots;

            query = query.AsNoTracking()
                         .Where(plot => plot.FarmId == farmId);

            return await query.ToArrayAsync();
        }
    }
}
