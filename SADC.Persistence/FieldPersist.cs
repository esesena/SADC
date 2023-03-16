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
    public class FieldPersist : IFieldPersist
    {
        private readonly SADCContext _context;

        public FieldPersist(SADCContext context)
        {
            _context = context;
        }

        public async Task<Field> GetFieldByIdsAsync(int fieldId, int farmId)
        {
            IQueryable<Field> query = _context.Fields;
            query = query.AsNoTracking()
                         .Where(field => field.FarmId == farmId
                                     && field.Id == fieldId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Field> GetFieldByIdAsync(int fieldId)
        {
            IQueryable<Field> query = _context.Fields;
            query = query.AsNoTracking()
                         .Where(field => field.Id == fieldId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Field[]> GetFieldsByFarmIdAsync(int farmId)
        {
            IQueryable<Field> query = _context.Fields;

            query = query.AsNoTracking()
                         .Where(field => field.FarmId == farmId);

            return await query.ToArrayAsync();
        }
    }
}
