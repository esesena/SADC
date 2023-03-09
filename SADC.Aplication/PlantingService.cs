﻿using AutoMapper;
using SADC.Application.Contracts;
using SADC.Application.Dtos;
using SADC.Domain;
using SADC.Persistence;
using SADC.Persistence.Contracts;
using SADC.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Application
{
    public class PlantingService : IPlantingService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IPlantingPersist _plantingPersist;
        private readonly IMapper _mapper;
        public PlantingService(IGeralPersist geralPersist,
                             IPlantingPersist palntingPersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _plantingPersist = palntingPersist;
            _mapper = mapper;
        }

        public async Task<PlantingDto> AddPlanting(PlantingDto model)
        {
            try
            {
                var planting = _mapper.Map<Planting>(model);

                _geralPersist.Add<Planting>(planting);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var plantingRetorno = await _plantingPersist.GetPlantingByIdAsync(planting.Id);

                    return _mapper.Map<PlantingDto>(plantingRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<bool> DeletePlanting(int plantingId)
        {
            try
            {
                var planting = await _plantingPersist.GetPlantingByIdAsync(plantingId);
                if (planting == null) throw new Exception("Fazenda para delete não encontrado.");

                _geralPersist.Delete<Planting>(planting);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PlantingDto> UpdatePlanting(int plantingId, PlantingDto model)
        {
            try
            {
                var planting = await _plantingPersist.GetPlantingByIdAsync(plantingId);
                if (planting == null) return null;

                model.Id = planting.Id;

                _mapper.Map(model, planting);

                _geralPersist.Update<Planting>(planting);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var plantingRetorno = await _plantingPersist.GetPlantingByIdAsync(planting.Id);

                    return _mapper.Map<PlantingDto>(plantingRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PlantingDto> GetPlantingByIdAsync(int plantingId)
        {
            try
            {
                var planting = await _plantingPersist.GetPlantingByIdAsync(plantingId);
                if (planting == null) return null;

                var resultado = _mapper.Map<PlantingDto>(planting);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<PlantingDto>> GetAllPlantingsAsync(PageParams pageParams)
        {
            try
            {
                var plantings = await _plantingPersist.GetAllPlantingsAsync(pageParams);
                if (plantings == null) return null;

                var resultado = _mapper.Map<PageList<PlantingDto>>(plantings);

                resultado.CurrentPage = plantings.CurrentPage;
                resultado.TotalPages = plantings.TotalPages;
                resultado.PageSize = plantings.PageSize;
                resultado.TotalCount = plantings.TotalCount;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
