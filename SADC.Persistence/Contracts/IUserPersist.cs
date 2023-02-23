using SADC.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.Persistence.Contracts
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> GetUserAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}
