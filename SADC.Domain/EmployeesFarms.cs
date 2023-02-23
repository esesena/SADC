using Microsoft.Extensions.Logging;

namespace SADC.Domain
{
    public class EmployeesFarms
    {
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}