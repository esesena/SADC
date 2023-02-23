using Microsoft.AspNetCore.Identity;
using SADC.Domain.Enums;

namespace SADC.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public Function Function { get; set; }
        public string ImageURL { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
