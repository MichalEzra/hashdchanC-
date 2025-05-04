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

        public async Task Save()
        {
            await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-IF22QMQ;database=hashadchanDb;trusted_connection=true;TrustServerCertificate=True");
        }
    }
}
