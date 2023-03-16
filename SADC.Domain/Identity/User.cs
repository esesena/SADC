using Microsoft.AspNetCore.Identity;
using SADC.Domain.Enums;

namespace SADC.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
