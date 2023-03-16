using SADC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Application.Dtos
{
    public class EmployeesFarmsDto
    {
        public int FarmId { get; set; }
        public FarmDto Farm { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
