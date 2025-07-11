using Common.Dto;
using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Repositories;
using Service.Interfaces;
using Service.Interfasces;
using Service.Service;
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
            services.AddScoped<IService<CandidateDto>, CandidateService>();
            //services.AddScoped<IUserLinkedService<CandidateDto>, CandidateService>();
            services.AddScoped<IService<MatchmakerDto>, MatchmakerService>();
            services.AddScoped<IUserLinkedService<MatchmakerDto>, MatchmakerService>();
            services.AddScoped<IService<UserDto>, UserService>();
            services.AddScoped<IService<MatchDto>, MatchService>();
            services.AddScoped<IServiceMatch, MatchService>();
            services.AddScoped<IMyDetails<Candidate>, CandidateService>();
            services.AddScoped<IHungarianAlgorithm, HungarianAlgorithmService>();
            services.AddScoped<IEmailService, SmtpEmailService>();


            services.AddAutoMapper(typeof(MyMapper));
            return services;
        }
    }
}
