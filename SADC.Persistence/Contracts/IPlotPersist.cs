using SADC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Persistence.Contracts
{
    public interface IPlotPersist
    {
        Task<Plot[]> GetPlotsByFarmIdAsync(int farmId);
        Task<Plot> GetPlotByIdsAsync(int plotId, int farmId);
    }
}
