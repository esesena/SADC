using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SADC.Domain
{
    public class PlantingField
    {
        public int PlantingId { get; set; }
        public Planting Planting { get; set; }
        public int FieldId { get; set; }
        public Field Field { get; set; }
    }
}
