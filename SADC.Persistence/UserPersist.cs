using Microsoft.EntityFrameworkCore;
using SADC.Domain.Identity;
using SADC.Persistence.Context;
using SADC.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly SADCContext _context;
        public UserPersist(SADCContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username.ToLower());
        }
    }
}
