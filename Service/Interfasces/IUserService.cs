using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;
using Repository.Entities;
using UserType = Common.Dto.UserType;

namespace Service.Interfasces
{
    public interface IUserService<T>
    {
        Task<List<UserDto>> GetUsersByType(UserType userType);
        Task<UserDto?> GetByEmail(string email);


    }
}
