using SADC.Domain.Enums;
using SADC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Application.Dtos
{
    public class PlantingDto
    {
        public int Id { get; set; }
        public DateTime PlantingDate { get; set; }
        public string Harvest { get; set; }
        public decimal RainAmount { get; set; }
        public string TypePlanting { get; set; }
        public string WeatherPlanting { get; set; }
        public decimal AirMoisture { get; set; }
        public int SeedId { get; set; }
        public SeedDto Seed { get; set; }
        public decimal SeedAmount { get; set; }
        public decimal SeedDistance { get; set; }
        public string Fertilizing { get; set; }
        public int FarmId { get; set; }
        public FarmDto Farm { get; set; }
        public IEnumerable<PlotDto> Plot { get; set; }

    }
}
