using SADC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SADC.Application.Dtos
{
    public class FieldDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Size { get; set; }
        public string SoilType { get; set; }
        public int FarmId { get; set; }
        public FarmDto Farm { get; set; }
        public IEnumerable<PlantingFieldDto> Planting { get; set; }
    }
}
