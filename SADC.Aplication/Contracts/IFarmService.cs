using SADC.Application.Dtos;
using SADC.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Application.Contracts
{
    public interface IFarmService
    {
        Task<FarmDto> AddFarms(FarmDto model);
        Task<FarmDto> UpdateFarm(int farmId, FarmDto model);
        Task<bool> DeleteFarm(int farmId);

        Task<PageList<FarmDto>> GetAllFarmsAsync(PageParams pageParams);
        Task<FarmDto> GetFarmByIdAsync(int farmId);
    }
}
