using AutoMapper;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Domain;
using SADC.Persistence.Contracts;
using SADC.Persistence.Models;

namespace SADC.Application
{
    public class SeedService : ISeedService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ISeedPersist _seedPersist;
        private readonly IMapper _mapper;
        public SeedService(IGeralPersist geralPersist,
                             ISeedPersist seedPersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _seedPersist = seedPersist;
            _mapper = mapper;
        }
        public async Task<SeedDto> AddSeed(SeedDto model)
        {
            try
            {
                var seed = _mapper.Map<Seed>(model);

                _geralPersist.Add<Seed>(seed);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var seedRetorno = await _seedPersist.GetSeedByIdAsync(seed.Id);

                    return _mapper.Map<SeedDto>(seedRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SeedDto> UpdateSeed(int seedId, SeedDto model)
        {
            try
            {
                var seed = await _seedPersist.GetSeedByIdAsync(seedId);
                if (seed == null) return null;

                model.Id = seed.Id;

                _mapper.Map(model, seed);

                _geralPersist.Update<Seed>(seed);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var seedRetorno = await _seedPersist.GetSeedByIdAsync(seed.Id);

                    return _mapper.Map<SeedDto>(seedRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteSeed(int seedId)
        {
            try
            {
                var seed = await _seedPersist.GetSeedByIdAsync(seedId);
                if (seed == null) throw new Exception("Semente para delete não encontrado.");

                _geralPersist.Delete<Seed>(seed);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<SeedDto>> GetAllSeedsAsync(PageParams pageParams)
        {
            try
            {
                var seeds = await _seedPersist.GetAllSeedsAsync(pageParams);
                if (seeds == null) return null;

                var resultado = _mapper.Map<PageList<SeedDto>>(seeds);

                resultado.CurrentPage = seeds.CurrentPage;
                resultado.TotalPages = seeds.TotalPages;
                resultado.PageSize = seeds.PageSize;
                resultado.TotalCount = seeds.TotalCount;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SeedDto> GetSeedByIdAsync(int seedId)
        {
            try
            {
                var seed = await _seedPersist.GetSeedByIdAsync(seedId);
                if (seed == null) return null;

                var resultado = _mapper.Map<SeedDto>(seed);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SeedDto> GetSeedForSelectAsync()
        {
            try
            {
                var seed = await _seedPersist.GetSeedForSelectAsync();
                if (seed == null) return null;

                var resultado = _mapper.Map<SeedDto>(seed);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
