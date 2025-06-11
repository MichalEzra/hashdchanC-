using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    //ממשק שמתאר את הנתונים 
    public interface IContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Matchmaker> Matchmakers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }
        public Task Save();
    }
}
