using SADC.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Application.Contracts
{
    public interface IFieldService
    {
        Task<FieldDto[]> SaveField(int farmId, FieldDto[] model);
        Task<bool> DeletField(int farmId, int fieldId);

        Task<FieldDto[]> GetFieldsByFarmIdAsync(int farmId);
        Task<FieldDto> GetFieldByIdsAsync(int farmId, int fieldId);
    }
}
