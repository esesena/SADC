using SADC.Domain.Enums;
using SADC.Domain.Identity;
using SADC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Application.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public Function Function { get; set; }
        public int Workload { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Cep { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<EmployeesFarms> EmployeesFarms { get; set; }
    }
}
