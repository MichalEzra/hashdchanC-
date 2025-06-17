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
    public class CandidateService : IService<CandidateDto>, IMyDetails<Candidate>, IUserLinkedService<CandidateDto>
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
        public async Task<Candidate[]> GetFemaleCandidatesAsync()
        {

            List<Candidate> allCandidates  = await repository.GetAll();
            List<Candidate> femaleCandidates = new List<Candidate>();

            foreach (Candidate candidate in allCandidates)
            {
                if (candidate.CandidateGender == Repository.Entities.Enums.Gender.FEMALE &&
                                  candidate.AvailableForProposals)
                {
                    femaleCandidates.Add(candidate);

                }
            }
            return femaleCandidates.ToArray();
        }

        public async Task<Candidate[]> GetMaleCandidatesAsync()
        {
            List<Candidate> allCandidates = await repository.GetAll();
            List<Candidate> maleCandidates = new List<Candidate>();

            foreach (Candidate candidate in allCandidates)
            {
                if (candidate.CandidateGender== Repository.Entities.Enums.Gender.MALE &&
                                  candidate.AvailableForProposals)
                {
                    maleCandidates.Add(candidate);

                }
            }
            return maleCandidates.ToArray();

        }

        public async Task<string> GetAllCandidateInfoAsync(Candidate candidate)
        {
            if (candidate == null)
                return "???? ?????? ???? ??????";

            StringBuilder generalInfo = new StringBuilder();

            generalInfo.AppendLine($"?? ???: {candidate.FirstName} {candidate.LastName}\n");
            generalInfo.AppendLine($"????: {candidate.CandidateGender}\n");
            generalInfo.AppendLine($"??? ????: {candidate.Status}\n");
            generalInfo.AppendLine($"???: {candidate.Age}\n");
            generalInfo.AppendLine($"???: {candidate.City}\n");
            generalInfo.AppendLine($"????: {candidate.CandidateSector}\n");
            generalInfo.AppendLine($"?? ????: {candidate.SubSector}\n");
            generalInfo.AppendLine($"??? ????? ????: {candidate.TorahLearning}\n");
            generalInfo.AppendLine($"???? ???????: {candidate.Education}\n");
            generalInfo.AppendLine($"?? ???? ???????: {candidate.StudyPlaceName}\n");
            generalInfo.AppendLine($"?????: {candidate.JobOrStudies}\n");
            generalInfo.AppendLine($"????: {candidate.Origin}\n");
            generalInfo.AppendLine($"????: {candidate.Languages}\n");
            generalInfo.AppendLine($"?????? ????: {candidate.ReligiousOpenness}\n");
            generalInfo.AppendLine($"????? ????: {candidate.ClothingStyle}\n");
            generalInfo.AppendLine($"????: {candidate.Height} ?\"?\n");
            generalInfo.AppendLine($"???? ???: {candidate.Physique}\n");
            generalInfo.AppendLine($"??? ???: {candidate.SkinTone}\n");
            generalInfo.AppendLine($"??? ????: {candidate.HairColor}\n");
            generalInfo.AppendLine($"??? ????: {candidate.Giving}\n");
            generalInfo.AppendLine($"??? ???? ????: {candidate.Expecting}\n");
            generalInfo.AppendLine($"??? ?????? ?????: {candidate.FamilyStatus}\n");
            generalInfo.AppendLine($"???? ??????: {(candidate.AvailableForProposals ? "??" : "??")}\n");
            generalInfo.AppendLine($"????? ??? ?????: {candidate.PreferredHeadCovering}\n");
            generalInfo.AppendLine($"??? ?????: {candidate.CandidatePhoneType}\n");

            if (candidate.CandidateGender == Repository.Entities.Enums.Gender.MALE)
            {
                generalInfo.AppendLine($"?????? ?????: {(candidate.License ? "??" : "??")}\n");
                generalInfo.AppendLine($"???: {(candidate.Beard ? "??" : "??")}\n");
                generalInfo.AppendLine($"????? ?????: {candidate.SmokingStatus}\n");
            }

            generalInfo.AppendLine($"?? ???? ????? ????: {candidate.RezumehName ?? "???"}\n");
            generalInfo.AppendLine($"????? ????: {candidate.DescriptionSelf}\n");
            generalInfo.AppendLine($"?? ????: {candidate.DescriptionFind}\n");

            return generalInfo.ToString();
        }

        public async Task<string> GetGeneralCandidateInfoAsync(Candidate candidate)
        {
            if (candidate == null)
                return "???? ?????? ???? ??????";

            StringBuilder generalInfo = new StringBuilder();

            generalInfo.AppendLine($"???: {candidate.Age}\n");
            generalInfo.AppendLine($"???: {candidate.City}\n");
            generalInfo.AppendLine($"??? ????: {candidate.Status}\n");
            generalInfo.AppendLine($"????: {candidate.CandidateSector}\n");
            generalInfo.AppendLine($"?? ????: {candidate.SubSector}\n");
            generalInfo.AppendLine($"????: {candidate.Origin}\n");
            generalInfo.AppendLine($"?????? ????: {candidate.ReligiousOpenness}\n");
            generalInfo.AppendLine($"????? ????: {candidate.ClothingStyle}\n");
            generalInfo.AppendLine($"?????: {candidate.JobOrStudies}\n");
            generalInfo.AppendLine($"???? ???????: {candidate.Education}\n");
            generalInfo.AppendLine($"?? ???? ????????: {candidate.StudyPlaceName}\n");
            if (candidate.CandidateGender == Repository.Entities.Enums.Gender.FEMALE)
            {
                generalInfo.AppendLine($"?????/???????: {candidate.JobOrStudies}\n");
                generalInfo.AppendLine($"????? ??? ?????: {candidate.PreferredHeadCovering}\n");
            }
            else
            {
                generalInfo.AppendLine($"??? ????? ????: {candidate.TorahLearning}\n");
                generalInfo.AppendLine($"?????? ?????: {(candidate.License ? "??" : "??")}\n");
                generalInfo.AppendLine($"???: {(candidate.Beard ? "??" : "??")}\n");
                generalInfo.AppendLine($"????? ?????: {candidate.SmokingStatus}\n");
            }

            generalInfo.AppendLine($"?? ??? ????: {candidate.DescriptionFind}\n");

            return generalInfo.ToString();
        }

        
    }
}





