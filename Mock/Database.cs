using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock
{
    public class Database : DbContext ,IContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Matchmaker> Matchmakers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }

        public async Task Save()
        {
            await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
<<<<<<< HEAD
            optionsBuilder.UseSqlServer("server=HPG101223;database=hashadchanDb;trusted_connection=true;TrustServerCertificate=True");
=======
            optionsBuilder.UseSqlServer("server=.;database=hashadchanDb;trusted_connection=true;TrustServerCertificate=True");
>>>>>>> c0ad84133a8aa652079eee6333ba9a477e5a3f30
        }
    }
}
