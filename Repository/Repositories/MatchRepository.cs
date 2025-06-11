using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MatchRepository : IRepository<Match>
    {
        private readonly IContext context;

        public MatchRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<Match> AddItem(Match item)
        {
            context.Matches.Add(item);
            await context.Save(); // נניח ש-Save גם היא Task
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var match = await GetById(id);
            context.Matches.Remove(match);
            await context.Save();
        }

        public async Task<List<Match>> GetAll()
        {
            return await Task.FromResult(context.Matches.ToList());
        }

        public async Task<Match> GetById(int id)
        {
            return await Task.FromResult(context.Matches.FirstOrDefault(x => x.Id == id));
        }

        public async Task UpdateItem(int id, Match item)
        {
            Match match = await GetById(id);
            match.DateMatch = DateTime.Now;
            match.Status = item.Status;
            match.IsEngaged = item.IsEngaged;
            match.ConfirmationGuy = item.ConfirmationGuy;
            match.ConfirmationGirl = item.ConfirmationGirl;
            await context.Save();
        }
    }
}

