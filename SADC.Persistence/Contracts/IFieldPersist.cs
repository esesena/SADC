using SADC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Persistence.Contracts
{
    public interface IFieldPersist
    {
        Task<Field[]> GetFieldsByFarmIdAsync(int farmId);
        Task<Field> GetFieldByIdAsync(int fieldId);
        Task<Field> GetFieldByIdsAsync(int fieldId, int farmId);
    }
}
