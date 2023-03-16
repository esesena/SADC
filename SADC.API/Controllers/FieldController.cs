using SADC.Application.Contracts;
using SADC.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace SADC.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FieldController : Controller
    {
        private readonly IFieldService _fieldService;

        public FieldController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet("{farmId}")]
        public async Task<IActionResult> Get(int farmId)
        {
            try
            {
                var fields = await _fieldService.GetFieldsByFarmIdAsync(farmId);
                if (fields == null) return NoContent();

                return Ok(fields);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Fields. Erro: {ex.Message}");
            }
        }

        [HttpPut("{farmId}")]
        public async Task<IActionResult> SaveFields(int farmId, FieldDto[] models)
        {
            try
            {
                var field = await _fieldService.SaveField(farmId, models);
                if (field == null) return NoContent();

                return Ok(field);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar Fields. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{farmId}/{fieldId}")]
        public async Task<IActionResult> Delete(int farmId, int fieldId)
        {
            try
            {
                var field = await _fieldService.GetFieldByIdsAsync(farmId, fieldId);
                if (field == null) return NoContent();


                return await _fieldService.DeletField(field.FarmId, field.Id) ?
                Ok(new { message = "Deletado" }) :
                throw new Exception("Ocorreu um problema não especifico ao tentar deletar Field.");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar Farms. Erro: {ex.Message}");
            }
        }
    }
}
