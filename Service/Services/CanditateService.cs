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
    public class CanditateService:IService<CandidateDto>
    {
        private readonly IRepository<Candidate> repository;
        private readonly IMapper mapper;
        public CanditateService(IRepository<Candidate> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public CandidateDto AddItem(CandidateDto item)
        {
            return mapper.Map<Candidate, CandidateDto>(repository.AddItem(mapper.Map<CandidateDto, Candidate>(item)));
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<CandidateDto> GetAll()
        {
            return mapper.Map<List<Candidate>, List<CandidateDto>>(repository.GetAll());
        }

        public CandidateDto GetById(int id)
        {
            return mapper.Map<Candidate, CandidateDto>(repository.GetById(id));
        }

        public void UpdateItem(int id, CandidateDto item)
        {
            repository.UpdateItem(id, mapper.Map<CandidateDto, Candidate>(item));
        }
    }
}
