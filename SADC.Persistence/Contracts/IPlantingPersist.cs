using SADC.Domain;
using SADC.Persistence.Models;

namespace SADC.Persistence.Contracts
{
    public interface IPlantingPersist
    {
        Task<PageList<Planting>> GetAllPlantingsAsync(PageParams pageParams, bool includePlots = false);
    }
}
