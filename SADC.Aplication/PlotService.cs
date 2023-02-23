using AutoMapper;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Domain;
using SADC.Persistence;
using SADC.Persistence.Contracts;
using Microsoft.Extensions.Logging;

namespace SADC.Application
{
    public class PlotService : IPlotService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IPlotPersist _plotPersist;
        private readonly IMapper _mapper;

        public PlotService(IGeralPersist geralPersist, IPlotPersist plotPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _plotPersist = plotPersist;
            _mapper = mapper;
        }

        public async Task AddPlot(int farmId, PlotDto model)
        {
            try
            {
                var plot = _mapper.Map<Plot>(model);
                plot.FarmId = farmId;

                _geralPersist.Add<Plot>(plot);

                await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PlotDto[]> SavePlot(int farmId, PlotDto[] models)
        {
            try
            {
                var plots = await _plotPersist.GetPlotsByFarmIdAsync(farmId);
                if (plots == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddPlot(farmId, model);
                    }
                    else
                    {
                        var plot = plots.FirstOrDefault(plot => plot.Id == model.Id);

                        model.FarmId = farmId;

                        _mapper.Map(model, plot);

                        _geralPersist.Update<Plot>(plot);

                        await _geralPersist.SaveChangesAsync();
                    }
                }

                var plotRetorno = await _plotPersist.GetPlotsByFarmIdAsync(farmId);

                return _mapper.Map<PlotDto[]>(plotRetorno);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletPlot(int farmId, int plotId)
        {
            try
            {
                var plot = await _plotPersist.GetPlotByIdsAsync(farmId, plotId);
                if (plot == null) throw new Exception("Talhão para delete não encontrado.");

                _geralPersist.Delete<Plot>(plot);
                return (await _geralPersist.SaveChangesAsync());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PlotDto> GetPlotByIdsAsync(int farmId, int plotId)
        {
            try
            {
                var plot = await _plotPersist.GetPlotByIdsAsync(farmId, plotId);
                if (plot == null) return null;

                var resultado = _mapper.Map<PlotDto>(plot);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PlotDto[]> GetPlotsByFarmIdAsync(int farmId)
        {
            try
            {
                var plots = await _plotPersist.GetPlotsByFarmIdAsync(farmId);
                if (plots == null) return null;

                var resultado = _mapper.Map<PlotDto[]>(plots);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
