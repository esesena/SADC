using SADC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Application.Dtos
{
    public class PlantingFieldDto
    {
        public int PlantingId { get; set; }
        public PlantingDto Planting { get; set; }
        public int FieldId { get; set; }
        public FieldDto Field { get; set; }
    }
}
