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
    public class CandidateService : IService<CandidateDto>, IUserLinkedService<CandidateDto>
    {
        private readonly IRepository<Candidate> repository;
        private readonly IMapper mapper;
        private readonly TextAnalyticsService _textAnalytics; // שירות ניתוח טקסט
        public CandidateService(IRepository<Candidate> repository, IMapper mapper, TextAnalyticsService textAnalytics)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._textAnalytics = textAnalytics;
        }

        public async Task<CandidateDto> AddItem(CandidateDto item)
        {
            // מחלץ מילות מפתח מתוך התיאור העצמי
            var selfKeywords = _textAnalytics.ExtractKeyPhrases(item.DescriptionSelf);
            item.DescriptionSelf = string.Join(',', selfKeywords);

            // מחלץ מילות מפתח מתוך תיאור מה שהוא מחפש
            var findKeywords = _textAnalytics.ExtractKeyPhrases(item.DescriptionFind);
            item.DescriptionFind = string.Join(',', findKeywords);

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
            // מחלץ מילות מפתח מתוך התיאור העצמי
            var selfKeywords = _textAnalytics.ExtractKeyPhrases(item.DescriptionSelf);
            item.DescriptionSelf = string.Join(',', selfKeywords);

            // מחלץ מילות מפתח מתוך תיאור מה שהוא מחפש
            var findKeywords = _textAnalytics.ExtractKeyPhrases(item.DescriptionFind);
            item.DescriptionFind = string.Join(',', findKeywords);

            await repository.UpdateItem(id, mapper.Map<CandidateDto, Candidate>(item));
        }

        public async Task<CandidateDto?> GetByUserId(int userId)
        {
            var candidate = await repository.GetAll();
            var found = candidate.FirstOrDefault(c => c.UserId == userId);
            return mapper.Map<CandidateDto>(found);
        }

    }
}
