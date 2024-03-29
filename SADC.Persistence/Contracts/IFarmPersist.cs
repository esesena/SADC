﻿using SADC.Domain;
using SADC.Persistence.Models;

namespace SADC.Persistence.Contracts
{
    public interface IFarmPersist
    {
        Task<PageList<Farm>> GetAllFarmsAsync(PageParams pageParams);

        Task<Farm> GetFarmByIdAsync(int farmId);
    }
}
