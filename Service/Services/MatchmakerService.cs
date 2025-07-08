using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MatchmakerService : IService<MatchmakerDto>, IUserLinkedService<MatchmakerDto>
    {
        private readonly IRepository<Matchmaker> repository;
        private readonly IMapper mapper;

        public MatchmakerService(IRepository<Matchmaker> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MatchmakerDto> AddItem(MatchmakerDto item)
        {
            return mapper.Map<Matchmaker, MatchmakerDto>(await repository.AddItem(mapper.Map<MatchmakerDto, Matchmaker>(item)));
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<MatchmakerDto>> GetAll()
        {
            return mapper.Map<List<Matchmaker>, List<MatchmakerDto>>(await repository.GetAll());
        }

        public async Task<MatchmakerDto> GetById(int id)
        {
            return mapper.Map<Matchmaker, MatchmakerDto>(await repository.GetById(id));
        }

        public async Task UpdateItem(int id, MatchmakerDto item)
        {
            await repository.UpdateItem(id, mapper.Map<MatchmakerDto, Matchmaker>(item));
        }
        public async Task<List<MatchmakerDto>> GetAllByUserId(int userId)
        {
            var all = await repository.GetAll();
            var matchmakers = all.Where(m => m.UserId == userId).ToList();
            return mapper.Map<List<MatchmakerDto>>(matchmakers);
        }


    }

}
