using AutoMapper;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Domain;
using SADC.Persistence.Contracts;
using SADC.Persistence.Models;

namespace SADC.Application
{
    public class FarmService : IFarmService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IFarmPersist _farmPersist;
        private readonly IMapper _mapper;
        public FarmService(IGeralPersist geralPersist,
                             IFarmPersist farmPersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _farmPersist = farmPersist;
            _mapper = mapper;
        }

        public async Task<FarmDto> AddFarms(FarmDto model)
        {
            try
            {
                var farm = _mapper.Map<Farm>(model);

                _geralPersist.Add<Farm>(farm);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var farmRetorno = await _farmPersist.GetFarmByIdAsync(farm.Id);

                    return _mapper.Map<FarmDto>(farmRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FarmDto> UpdateFarm(int farmId, FarmDto model)
        {
            try
            {
                var farm = await _farmPersist.GetFarmByIdAsync(farmId);
                if (farm == null) return null;

                model.Id = farm.Id;

                _mapper.Map(model, farm);

                _geralPersist.Update<Farm>(farm);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var farmRetorno = await _farmPersist.GetFarmByIdAsync(farm.Id);

                    return _mapper.Map<FarmDto>(farmRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFarm(int farmId)
        {
            try
            {
                var farm = await _farmPersist.GetFarmByIdAsync(farmId);
                if (farm == null) throw new Exception("Fazenda para delete não encontrado.");

                _geralPersist.Delete<Farm>(farm);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<FarmDto>> GetAllFarmsAsync(PageParams pageParams)
        {
            try
            {
                var farms = await _farmPersist.GetAllFarmsAsync(pageParams);
                if (farms == null) return null;

                var resultado = _mapper.Map<PageList<FarmDto>>(farms);

                resultado.CurrentPage = farms.CurrentPage;
                resultado.TotalPages = farms.TotalPages;
                resultado.PageSize = farms.PageSize;
                resultado.TotalCount = farms.TotalCount;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FarmDto> GetFarmByIdAsync(int farmId)
        {
            try
            {
                var farm = await _farmPersist.GetFarmByIdAsync(farmId);
                if (farm == null) return null;

                var resultado = _mapper.Map<FarmDto>(farm);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
