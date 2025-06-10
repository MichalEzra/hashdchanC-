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
    public class CandidateService : IService<CandidateDto>, ICandidatesDetails<Candidate>, IUserLinkedService<CandidateDto>
    {
        private readonly IRepository<Candidate> repository;
        private readonly IMapper mapper;
        public CandidateService(IRepository<Candidate> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CandidateDto> AddItem(CandidateDto item)
        {
            var addedCandidate = await repository.AddItem(mapper.Map<CandidateDto, Candidate>(item));
            return mapper.Map<Candidate, CandidateDto>(addedCandidate);
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

        public async Task<CandidateDto?> GetByUserId(int userId)
        {
            var candidate = await repository.GetAll();
            var found = candidate.FirstOrDefault(c => c.UserId == userId);
            return mapper.Map<CandidateDto>(found);
        }
        //public async Task<CandidateDto[]> GetFemaleCandidatesAsync()
        //{
        //    var allCandidates = await GetAll();

        //    return allCandidates
        //        .Where(candidate => candidate.CandidateGender == Gender.FEMALE &&
        //        candidate.AvailableForProposals)
        //        .ToArray();
        //}

        //public async Task<CandidateDto[]> GetMaleCandidatesAsync()
        //{
        //    var allCandidates = await GetAll();

        //    return allCandidates
        //        .Where(candidate => candidate.CandidateGender == Gender.MALE &&
        //        candidate.AvailableForProposals)
        //        .ToArray();
        //}
        // החזרת מועמדות נשים
        public async Task<Candidate[]> GetFemaleCandidatesAsync()
        {
            var allCandidates = await repository.GetAll();
            return allCandidates
                .Where(candidate => candidate.CandidateGender.Equals(Gender.FEMALE) &&
                                  candidate.AvailableForProposals)
                .ToArray();
        }

        public async Task<Candidate[]> GetMaleCandidatesAsync()

        {
            var allCandidates = await repository.GetAll();
            return allCandidates
                .Where(candidate => candidate.CandidateGender.Equals(Gender.MALE) &&
                                  candidate.AvailableForProposals)
                .ToArray();
        }


    }
}





