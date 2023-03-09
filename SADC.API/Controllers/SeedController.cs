using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SADC.API.Extensions;
using SADC.API.Helpers;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Persistence.Models;

namespace SADC.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : Controller
    {
        private readonly ISeedService _seedService;
        private readonly IUtil _util;
        private readonly IAccountService _accountService;

        public SeedController(ISeedService seedService, IUtil util, IAccountService accountService)
        {
            _seedService = seedService;
            _util = util;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            try
            {
                var seeds = await _seedService.GetAllSeedsAsync(pageParams);
                if (seeds == null) return NoContent();

                Response.AddPagination(seeds.CurrentPage, seeds.PageSize, seeds.TotalCount, seeds.TotalPages);

                return Ok(seeds);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar plantação. Erro: {ex.Message}");
            }
        }

        [HttpGet("select")]
        public async Task<IActionResult> GetForSelect()
        {
            try
            {
                var seeds = await _seedService.GetSeedForSelectAsync();
                if (seeds == null) return NoContent();

                return Ok(seeds);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar sementes. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var seed = await _seedService.GetSeedByIdAsync(id);
                if (seed == null) return NoContent();

                return Ok(seed);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar plantação. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(SeedDto model)
        {
            try
            {
                var seed = await _seedService.AddSeed(model);
                if (seed == null) return NoContent();

                return Ok(seed);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar Semente. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SeedDto model)
        {
            try
            {
                var seed = await _seedService.UpdateSeed(id, model);
                if (seed == null) return NoContent();

                return Ok(seed);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar Semente. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var seed = await _seedService.GetSeedByIdAsync(id);
                if (seed == null) return NoContent();


                if (await _seedService.DeleteSeed(id))
                {
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Ocorreu um problema não especifico ao tentar deletar Semente.");
                }

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar Seeds. Erro: {ex.Message}");
            }
        }
    }
}
