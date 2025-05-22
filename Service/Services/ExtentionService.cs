using Common.Dto;
using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Repositories;
using Service.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public static class ExtentionService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddRepositorys();
            services.AddScoped<IService<CandidateDto>, CanditateService>();
            services.AddScoped<IUserLinkedService<CandidateDto>, CanditateService>();
            services.AddScoped<IService<MatchmakerDto>, MatchmakerService>();
            services.AddScoped<IUserLinkedService<MatchmakerDto>, MatchmakerService>();
            services.AddScoped<IService<UserDto>, UserService>();
            services.AddAutoMapper(typeof(MyMapper));
            return services;
        }
    }
}
