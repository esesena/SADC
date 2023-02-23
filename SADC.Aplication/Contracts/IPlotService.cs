using SADC.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Application.Contracts
{
    public interface IPlotService
    {
        Task<PlotDto[]> SavePlot(int farmId, PlotDto[] model);
        Task<bool> DeletPlot(int farmId, int plotId);

        Task<PlotDto[]> GetPlotsByFarmIdAsync(int farmId);
        Task<PlotDto> GetPlotByIdsAsync(int farmId, int plotId);
    }
}
