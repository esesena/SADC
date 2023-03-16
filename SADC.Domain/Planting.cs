using SADC.Domain.Enums;

namespace SADC.Domain
{
    public class Planting
    { 
        public int Id { get; set; }
        public DateTime PlantingDate { get; set; }
        public string Harvest { get; set; }
        public decimal RainAmount { get; set; }
        public TypePlanting TypePlanting { get; set; }
        public string WeatherPlanting{ get; set; }
        public decimal AirMoisture { get; set; }
        public int SeedId { get; set; }
        public Seed Seed { get; set; }
        public decimal SeedAmount { get; set; }
        public string Fertilizing { get; set; }
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
        public IEnumerable<PlantingField> Fields { get; set; }

    }
}
