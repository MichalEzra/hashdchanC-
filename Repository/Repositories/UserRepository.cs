using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Repository.Repositories
{
    public class UserRepository : IRepository<User> , IUserRepository<User>
    {
        private readonly IContext context;

        public UserRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<User> AddItem(User item)
        {
            await context.Users.AddAsync(item);
            await context.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var user = await GetById(id);
            if (user == null)
                throw new Exception("User not found");

            context.Users.Remove(user);
            await context.Save();
        }

        public async Task<List<User>> GetAll()
        {
            return await context.Users
                .Include(u => u.Candidates) // חשוב - כלול את המועמדים
                .ToListAsync();
        }


        public async Task<User> GetById(int id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateItem(int id, User updatedUser)
        {
            var user = await GetById(id);
            if (user == null)
                throw new Exception("User not found");

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.UserType = updatedUser.UserType;
            user.Candidates = updatedUser.Candidates;
            
            await context.Save();
        }
        public async Task<List<User>> GetUsersByTypeFromRepository(UserType dtoUserType)
        {

            var entityType = (Repository.Entities.UserType)dtoUserType;
            return await context.Users
                                 .Where(u => u.UserType == entityType)
                                 .ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }


        //Task<List<User>> IUserRepository<User>.GetUsersByTypeFromRepository(UserType type)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
