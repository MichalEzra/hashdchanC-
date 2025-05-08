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
    public class CanditateService : IService<CandidateDto>
    {
        private readonly IRepository<Candidate> repository;
        private readonly IMapper mapper;
        public CanditateService(IRepository<Candidate> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CandidateDto> AddItem(CandidateDto item)
        {
            return mapper.Map<Candidate, CandidateDto>(await repository.AddItem(mapper.Map<CandidateDto, Candidate>(item)));
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<CandidateDto>> GetAll()
        {
            return mapper.Map<List<Candidate>, List<CandidateDto>>(await repository.GetAll());
        }

        public async Task<CandidateDto> GetById(int id)
        {
            return mapper.Map<Candidate, CandidateDto>(await repository.GetById(id));
        }

        public async Task UpdateItem(int id, CandidateDto item)
        {
            await repository.UpdateItem(id, mapper.Map<CandidateDto, Candidate>(item));
        }
    }
}
