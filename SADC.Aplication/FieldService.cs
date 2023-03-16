using AutoMapper;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Domain;
using SADC.Persistence;
using SADC.Persistence.Contracts;
using Microsoft.Extensions.Logging;

namespace SADC.Application
{
    public class FieldService : IFieldService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IFieldPersist _fieldPersist;
        private readonly IMapper _mapper;

        public FieldService(IGeralPersist geralPersist, IFieldPersist fieldPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _fieldPersist = fieldPersist;
            _mapper = mapper;
        }

        public async Task AddField(int farmId, FieldDto model)
        {
            try
            {
                var field = _mapper.Map<Field>(model);
                field.FarmId = farmId;

                _geralPersist.Add<Field>(field);

                await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<FieldDto[]> SaveField(int farmId, FieldDto[] models)
        {
            try
            {
                var fields = await _fieldPersist.GetFieldsByFarmIdAsync(farmId);
                if (fields == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddField(farmId, model);
                    }
                    else
                    {
                        var field = fields.FirstOrDefault(field => field.Id == model.Id);

                        model.FarmId = farmId;

                        _mapper.Map(model, field);

                        _geralPersist.Update<Field>(field);

                        await _geralPersist.SaveChangesAsync();
                    }
                }

                var fieldRetorno = await _fieldPersist.GetFieldsByFarmIdAsync(farmId);

                return _mapper.Map<FieldDto[]>(fieldRetorno);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletField(int farmId, int fieldId)
        {
            try
            {
                var field = await _fieldPersist.GetFieldByIdsAsync(farmId, fieldId);
                if (field == null) throw new Exception("Talhão para delete não encontrado.");

                _geralPersist.Delete<Field>(field);
                return (await _geralPersist.SaveChangesAsync());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<FieldDto> GetFieldByIdsAsync(int farmId, int fieldId)
        {
            try
            {
                var field = await _fieldPersist.GetFieldByIdsAsync(farmId, fieldId);
                if (field == null) return null;

                var resultado = _mapper.Map<FieldDto>(field);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FieldDto[]> GetFieldsByFarmIdAsync(int farmId)
        {
            try
            {
                var fields = await _fieldPersist.GetFieldsByFarmIdAsync(farmId);
                if (fields == null) return null;

                var resultado = _mapper.Map<FieldDto[]>(fields);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
