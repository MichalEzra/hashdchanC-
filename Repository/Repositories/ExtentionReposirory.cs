using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public static class ExtentionReposirory
    {
        //מצביע למחלקה 
        public static IServiceCollection AddRepositorys(this IServiceCollection services)
        { 
            // יוצר מופע עבור כל גולש ומזריק אותו למחלקה שמממשת את הממשק הזה 
            services.AddScoped<IRepository<Candidate>, CandidateRepository>();
            services.AddScoped<IRepository<Matchmaker>, MatchmakerRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Match>, MatchRepository>();
            return services;
        }
    }
}
