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
    public class MatchmakerRepository : IRepository<Matchmaker>
    {
        private readonly IContext context;
        public MatchmakerRepository(IContext context)
        {
            this.context = context;
        }
        public async Task<Matchmaker> AddItem(Matchmaker item)
        {
            await context.Matchmakers.AddAsync(item);
            await context.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var matchmaker = await GetById(id);
            if (matchmaker == null)
            {
                throw new Exception("Matchmaker not found");
            }
            this.context.Matchmakers.Remove(matchmaker);
            await this.context.Save();
        }

        public async Task<List<Matchmaker>> GetAll()
        {
            return await context.Matchmakers.ToListAsync();
        }

        public async Task<Matchmaker> GetById(int id)
        {
            return await context.Matchmakers.Include(m => m.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(int id, Matchmaker item)
        {
            var matchmaker = await GetById(id);
            if (matchmaker == null)
            {
                throw new Exception("Matchmaker not found");
            }

            matchmaker.UserId = item.UserId;
            matchmaker.FirstName = item.FirstName;
            matchmaker.LastName = item.LastName;
            matchmaker.BirthDate = item.BirthDate;
            matchmaker.MatchmakerGender = item.MatchmakerGender;
            matchmaker.IdentityNumber = item.IdentityNumber;
            matchmaker.MarriageDate = item.MarriageDate;
            matchmaker.Country = item.Country;
            matchmaker.City = item.City;
            matchmaker.MatchmakerSector = item.MatchmakerSector;
            matchmaker.SubSector = item.SubSector;
            matchmaker.YearsOfExperience = item.YearsOfExperience;
            matchmaker.MatchesClosed = item.MatchesClosed;
            matchmaker.Languages = item.Languages;
            matchmaker.ReligiousOpenness = item.ReligiousOpenness;
            matchmaker.PhoneNumber = item.PhoneNumber;

            await context.Save();
        }
    }
}

