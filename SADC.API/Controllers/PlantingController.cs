using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SADC.API.Extensions;
using SADC.API.Helpers;
using SADC.Application;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Persistence.Models;

namespace SADC.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlantingController : Controller
    {
        private readonly IPlantingService _plantingService;
        private readonly IUtil _util;
        private readonly IAccountService _accountService;

        public PlantingController(IPlantingService plantingService, IUtil util, IAccountService accountService)
        {
            _plantingService = plantingService;
            _util = util;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            try
            {
                var plantings = await _plantingService.GetAllPlantingsAsync(pageParams);
                if (plantings == null) return NoContent();

                Response.AddPagination(plantings.CurrentPage, plantings.PageSize, plantings.TotalCount, plantings.TotalPages);

                return Ok(plantings);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar plantação. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var planting = await _plantingService.GetPlantingByIdAsync(id);
                if (planting == null) return NoContent();

                return Ok(planting);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar plantação. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PlantingDto model)
        {
            try
            {
                var planting = await _plantingService.AddPlanting(model);
                if (planting == null) return NoContent();

                return Ok(planting);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar Plantação. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PlantingDto model)
        {
            try
            {
                var planting = await _plantingService.UpdatePlanting(id, model);
                if (planting == null) return NoContent();

                return Ok(planting);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar Plantação. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var planting = await _plantingService.GetPlantingByIdAsync(id);
                if (planting == null) return NoContent();


                if (await _plantingService.DeletePlanting(id))
                {
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Ocorreu um problema não especifico ao tentar deletar Plantação.");
                }

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar Plantings. Erro: {ex.Message}");
            }
        }
    }
}
