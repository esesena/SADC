using SADC.Domain.Identity;
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
    public class FarmDto
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageURL { get; set; }
        public double Size { get; set; }

        public IEnumerable<EmployeesFarms> EmployeesFarms { get; set; }
        public IEnumerable<Plot> Plots { get; set; }
    }
}
