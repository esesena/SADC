using SADC.Application.Dtos;
using SADC.Persistence.Models;

namespace SADC.Application.Contracts
{
    public interface IPlantingService
    {
        Task<PlantingDto> AddPlanting(PlantingDto model);
        Task<PlantingDto> UpdatePlanting(int plantingId, PlantingDto model);
        Task<bool> DeletePlanting(int plantingId);

        Task<PageList<PlantingDto>> GetAllPlantingsAsync(PageParams pageParams);
        Task<PlantingDto> GetPlantingByIdAsync(int plantingId);
    }
}
