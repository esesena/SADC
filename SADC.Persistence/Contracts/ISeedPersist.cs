using SADC.Domain;
using SADC.Persistence.Models;

namespace SADC.Persistence.Contracts
{
    public interface ISeedPersist
    {
        Task<PageList<Seed>> GetAllSeedsAsync(PageParams pageParams);
        Task<Seed> GetSeedByIdAsync(int seedId);
        Task<Seed> GetSeedForSelectAsync();
    }
}
