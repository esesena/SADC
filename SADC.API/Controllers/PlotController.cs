using SADC.Application.Contracts;
using SADC.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace SADC.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlotController : Controller
    {
        private readonly IPlotService _plotService;

        public PlotController(IPlotService plotService)
        {
            _plotService = plotService;
        }

        [HttpGet("{farmId}")]
        public async Task<IActionResult> Get(int farmId)
        {
            try
            {
                var plots = await _plotService.GetPlotsByFarmIdAsync(farmId);
                if (plots == null) return NoContent();

                return Ok(plots);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Plots. Erro: {ex.Message}");
            }
        }

        [HttpPut("{farmId}")]
        public async Task<IActionResult> SavePlots(int farmId, PlotDto[] models)
        {
            try
            {
                var plot = await _plotService.SavePlot(farmId, models);
                if (plot == null) return NoContent();

                return Ok(plot);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar Plots. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{farmId}/{plotId}")]
        public async Task<IActionResult> Delete(int farmId, int plotId)
        {
            try
            {
                var plot = await _plotService.GetPlotByIdsAsync(farmId, plotId);
                if (plot == null) return NoContent();


                return await _plotService.DeletPlot(plot.FarmId, plot.Id) ?
                Ok(new { message = "Deletado" }) :
                throw new Exception("Ocorreu um problema não especifico ao tentar deletar Plot.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar Farms. Erro: {ex.Message}");
            }
        }
    }
}
