using SADC.API.Extensions;
using SADC.API.Helpers;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SADC.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : Controller
    {
        private readonly IFarmService _farmService;
        private readonly IUtil _util;
        private readonly IAccountService _accountService;
        private readonly string _destiny = "FarmImage";

        public FarmController(IFarmService farmService, IUtil util, IAccountService accountService)
        {
            _farmService = farmService;
            _util = util;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            try
            {
                var farms = await _farmService.GetAllFarmsAsync(pageParams);
                if (farms == null) return NoContent();

                Response.AddPagination(farms.CurrentPage, farms.PageSize, farms.TotalCount, farms.TotalPages);

                return Ok(farms);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar farms. Erro: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var farm = await _farmService.GetFarmByIdAsync(id);
                if (farm == null) return NoContent();

                return Ok(farm);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Farms. Erro: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(FarmDto model)
        {
            try
            {
                var farm = await _farmService.AddFarms(model);
                if (farm == null) return NoContent();

                return Ok(farm);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar Farms. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-image/{farmId}")]
        public async Task<IActionResult> UploadImage(int farmId)
        {
            try
            {
                var farm = await _farmService.GetFarmByIdAsync(farmId);
                if (farm == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _util.DeleteImage(farm.ImageURL, _destiny);
                    farm.ImageURL = await _util.SaveImage(file, _destiny);
                }
                var FarmRetorno = await _farmService.UpdateFarm(farmId, farm);

                return Ok(FarmRetorno);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar Farms. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FarmDto model)
        {
            try
            {
                var farm = await _farmService.UpdateFarm(id, model);
                if (farm == null) return NoContent();

                return Ok(farm);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar Farms. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var farm = await _farmService.GetFarmByIdAsync(id);
                if (farm == null) return NoContent();


                if (await _farmService.DeleteFarm(id))
                {
                    _util.DeleteImage(farm.ImageURL, _destiny);
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Ocorreu um problema não especifico ao tentar deletar Farm.");
                }

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar Farms. Erro: {ex.Message}");
            }
        }
    }
}
