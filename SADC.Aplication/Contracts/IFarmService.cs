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
        Task<FarmDto> AddFarms(int userId, FarmDto model);
        Task<FarmDto> UpdateFarm(int userId, int farmId, FarmDto model);
        Task<bool> DeleteFarm(int userId, int farmId);

        Task<PageList<FarmDto>> GetAllFarmsAsync(int userId, PageParams pageParams);
        Task<FarmDto> GetFarmByIdAsync(int userId, int farmId);
    }
}
