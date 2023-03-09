using SADC.Application.Dtos;
using SADC.Domain;
using SADC.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Application.Contracts
{
    public interface ISeedService
    {
        Task<SeedDto> AddSeed(SeedDto model);
        Task<SeedDto> UpdateSeed(int seedId, SeedDto model);
        Task<bool> DeleteSeed(int seedId);

        Task<PageList<SeedDto>> GetAllSeedsAsync(PageParams pageParams);
        Task<SeedDto> GetSeedByIdAsync(int seedId);
        Task<SeedDto> GetSeedForSelectAsync();
    }
}
