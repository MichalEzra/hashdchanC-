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
        public User AddItem(User item)
        {
            this.context.Users.Add(item);
            this.context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            var user = GetById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            this.context.Users.Remove(user);
            this.context.Save();
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }
        public void UpdateItem(int id, User updatedUser)
        {
            var user = GetById(id);
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

            context.Save();
        }

    }
}
