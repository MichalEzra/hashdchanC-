using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IContext context;
        public UserRepository(IContext context)
        {
            this.context = context;
        }
        public async Task<User> AddItem(User item)
        {
            await this.context.Users.AddAsync(item);
            await this.context.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var user =await GetById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            this.context.Users.Remove(user);
            await this.context.Save();
        }

        public async Task<List<User>> GetAll()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task UpdateItem(int id, User updatedUser)
        {
            var user = await GetById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.UserType = updatedUser.UserType;
            user.ContactPersonFirstName = updatedUser.ContactPersonFirstName;
            user.ContactPersonLastName = updatedUser.ContactPersonLastName;
            user.ContactPersonPhone = updatedUser.ContactPersonPhone;
            user.CandidatesList = updatedUser.CandidatesList ?? new List<Candidate>();

            await context.Save();
        }

     
    }
}
