using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;

namespace Repository.Interfaces
{
    public interface IUserRepository<T>
    {
        Task<List<User>> GetUsersByTypeFromRepository(UserType type);
        Task<User?> GetByEmail(string email);

    }
}
