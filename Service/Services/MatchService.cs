using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Repository.Entities;
using Repository.Interfaces;
using Common.Dto;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Service.Interfasces;
using Repository.Repositories;

namespace Service.Service
{
    public class MatchService : IService<MatchDto>, IServiceMatch
    {
        private readonly IRepository<Match> _repository;
        private readonly IRepository<Matchmaker> _repositoryMatchmaker;
        private readonly IRepository<Candidate> _repositoryCandidate;

        private readonly IMapper _mapper;

        public MatchService(IRepository<Match> repository, IMapper mapper, IRepository<Matchmaker> repositoryMatchmaker)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryMatchmaker = repositoryMatchmaker;
        }

        public async Task<MatchDto> AddItem(MatchDto item)
        {
            var match = await _repository.AddItem(_mapper.Map<Match>(item));
            return _mapper.Map<MatchDto>(match);
        }

        public async Task DeleteItem(int id)
        {
            await _repository.DeleteItem(id);
        }
        public async Task<List<MatchDto>> GetAll()
        {
            var matches = await _repository.GetAll();
            return _mapper.Map<List<MatchDto>>(matches);
        }

        public async Task<List<MatchDto>> GetAllMatchByIdCandidate(int id)
        {
            var matches = await _repository.GetAll();
            var filtered = matches
                .Where(x => x.IdCandidateGuy == id || x.IdCandidateGirl == id)
                .ToList();
            return _mapper.Map<List<MatchDto>>(filtered);
        }

        public async Task<MatchDto> GetById(int id)
        {
            var match = await _repository.GetById(id);
            return _mapper.Map<MatchDto>(match);
        }

        public async Task<List<MatchDto>> GetMatchesByIdMatchmaker(int id)
        {
            var matches = await _repository.GetAll();
            return _mapper.Map<List<MatchDto>>(matches.Where(x => x.IdMatchmaker == id).ToList());
        }

        public async Task<MatchDto> GetMatchByIdCandidates(int id1, int id2)
        {
            var matches = await _repository.GetAll();
            var match = matches.FirstOrDefault(m =>
                (m.IdCandidateGuy == id1 && m.IdCandidateGirl == id2) ||
                (m.IdCandidateGuy == id2 && m.IdCandidateGirl == id1));

            return _mapper.Map<MatchDto>(match);
        }

        public async Task UpdateItem(int id, MatchDto item)
        {
            await _repository.UpdateItem(id, _mapper.Map<Match>(item));
        }

        public async Task<List<EngagedMatchDto>> GetAllEngagedment()
        {
            var all = await GetAll(); // מחזיר את כל ההתאמות (MatchDto)

            var engaged = all.Where(x => x.IsEngaged).ToList();

            var result = new List<EngagedMatchDto>();

            foreach (var match in engaged)
            {
                var guy = await _repositoryCandidate.GetById(match.IdCandidateGuy);
                var girl = await _repositoryCandidate.GetById(match.IdCandidateGirl);

                result.Add(new EngagedMatchDto
                {
                    NameGuy = guy.FirstName + " " + guy.LastName,
                    NameGirl = girl.FirstName + " " + girl.LastName,
                    YeshivaGuy = guy.StudyPlaceName,
                    SeminaryGirl = girl.StudyPlaceName,
                    CityGuy = guy.City,
                    CityGirl = girl.City,
                    DateMatch = match.DateMatch
                });
            }

            return result;
        }
        //public async Task<List<MatchDto>> GetAllEngagedment()
        //{
        //    var all = await GetAll();
        //    return all.Where(x => x.IsEngaged == true).ToList();
        //}


        public async Task<List<Matchmaker>> GetAllMatchmakerActives()
        {
            var matches = await _repository.GetAll();
            var matchmakers = new List<Matchmaker>();

            foreach (var match in matches)
            {
                if (match.Active)
                {
                    var matchmaker = await _repositoryMatchmaker.GetById(match.IdMatchmaker);
                    if (!matchmakers.Any(m => m.Id == matchmaker.Id))
                        matchmakers.Add(matchmaker);
                }
            }

            return matchmakers;
        }


        //public async Task<List<EngagedMatchDto>> GetAllEngagedment()
        //{
        //    var all = await GetAll(); // מחזיר את כל ההתאמות (MatchDto)

        //    var engaged = all.Where(x => x.IsEngaged).ToList();

        //    var result = new List<EngagedMatchDto>();

        //    foreach (var match in engaged)
        //    {
        //        var guy = await _repositoryCandidate.GetById(match.IdCandidateGuy);
        //        var girl = await _repositoryCandidate.GetById(match.IdCandidateGirl);

        //        result.Add(new EngagedMatchDto
        //        {
        //            NameGuy = guy.FirstName + " " + guy.LastName,
        //            NameGirl = girl.FirstName + " " + girl.LastName,
        //            YeshivaGuy = guy.StudyPlaceName,
        //            SeminaryGirl = girl.StudyPlaceName,
        //            CityGuy = guy.City,
        //            CityGirl = girl.City,
        //            DateMatch = match.DateMatch
        //        });
        //    }

        //    return result;
        //}
    }
}
