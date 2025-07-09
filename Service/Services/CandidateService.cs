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
            var candidate = await repository.GetById(id);
            var dto = mapper.Map<CandidateDto>(candidate);

            // טען את התמונה רק אם הקובץ קיים
            try
            {
                if (!string.IsNullOrWhiteSpace(candidate.ImageUrl))
                {
                    var imagePath = Path.Combine(Environment.CurrentDirectory, "Images", candidate.ImageUrl);
                    if (File.Exists(imagePath))
                        dto.ArrImage = await File.ReadAllBytesAsync(imagePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error loading image: {ex.Message}");
            }

            return dto;
        }


        public async Task UpdateItem(int id, CandidateDto item)
        {
            await repository.UpdateItem(id, mapper.Map<CandidateDto, Candidate>(item));
        }

        //public async Task<List<CandidateDto>> GetAllByUserId(int userId)
        //{
        //    var candidates = await repository.GetAll();
        //    var filtered = candidates.Where(c => c.UserId == userId).ToList();
        //    return mapper.Map<List<CandidateDto>>(filtered);
        //}


        public async Task<Candidate[]> GetFemaleCandidatesAsync()
        {

            List<Candidate> allCandidates  = await repository.GetAll();
            List<Candidate> femaleCandidates = new List<Candidate>();

            foreach (Candidate candidate in allCandidates)
            {
                if (candidate.Gender == Repository.Entities.Enums.Gender.נקבה &&
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
                if (candidate.Gender== Repository.Entities.Enums.Gender.זכר &&
                                  candidate.AvailableForProposals)
                {
                    maleCandidates.Add(candidate);

                }
            }
            return maleCandidates.ToArray();

        }

        //נותן את כל הפרטים על המועמד 
        public async Task<string> GetAllCandidateInfoAsync(Candidate candidate)
        {
            if (candidate == null)
                return "פרטי המועמד אינם זמינים";

            StringBuilder generalInfo = new StringBuilder();

            generalInfo.AppendLine($"שם מלא: {candidate.FirstName} {candidate.LastName}\n");
            generalInfo.AppendLine($"מגדר: {candidate.Gender}\n");
            generalInfo.AppendLine($"מצב אישי: {candidate.Status}\n");
            generalInfo.AppendLine($"גיל: {candidate.Age}\n");
            generalInfo.AppendLine($"עיר: {candidate.City}\n");
            generalInfo.AppendLine($"מגזר: {candidate.CandidateSector}\n");
            generalInfo.AppendLine($"תת מגזר: {candidate.SubSector}\n");
            generalInfo.AppendLine($"רמת לימוד תורה: {candidate.TorahLearning}\n");
            generalInfo.AppendLine($"מוסד לימודים: {candidate.Education}\n");
            generalInfo.AppendLine($"שם מוסד לימודים: {candidate.StudyPlaceName}\n");
            generalInfo.AppendLine($"עיסוק: {candidate.JobOrStudies}\n");
            generalInfo.AppendLine($"מוצא: {candidate.Origin}\n");
            generalInfo.AppendLine($"שפות: {candidate.Languages}\n");
            generalInfo.AppendLine($"פתיחות דתית: {candidate.ReligiousOpenness}\n");
            generalInfo.AppendLine($"סגנון לבוש: {candidate.ClothingStyle}\n");
            generalInfo.AppendLine($"גובה: {candidate.Height} ס\"מ\n");
            generalInfo.AppendLine($"מבנה גוף: {candidate.Physique}\n");
            generalInfo.AppendLine($"צבע עור: {candidate.SkinTone}\n");
            generalInfo.AppendLine($"צבע שיער: {candidate.HairColor}\n");
            generalInfo.AppendLine($"כמה נותן: {candidate.Giving}\n");
            generalInfo.AppendLine($"כמה מצפה לקבל: {candidate.Expecting}\n");
            generalInfo.AppendLine($"מצב משפחתי הורים: {candidate.FamilyStatus}\n");
            generalInfo.AppendLine($"פנוי להצעות: {(candidate.AvailableForProposals ? "כן" : "לא")}\n");
            generalInfo.AppendLine($"כיסוי ראש מועדף: {candidate.PreferredHeadCovering}\n");
            generalInfo.AppendLine($"סוג טלפון: {candidate.CandidatePhoneType}\n");

            if (candidate.Gender == Repository.Entities.Enums.Gender.זכר)
            {
                generalInfo.AppendLine($"רישיון נהיגה: {(candidate.License ? "כן" : "לא")}\n");
                generalInfo.AppendLine($"זקן: {(candidate.Beard ? "כן" : "לא")}\n");
                generalInfo.AppendLine($"סטטוס עישון: {candidate.SmokingStatus}\n");
            }

            generalInfo.AppendLine($"שם קובץ קורות חיים: {candidate.RezumehName ?? "אין"}\n");
            generalInfo.AppendLine($"תיאור עצמי: {candidate.DescriptionSelf}\n");
            generalInfo.AppendLine($"מה מחפש: {candidate.DescriptionFind}\n");

            return generalInfo.ToString();
        }
        //נותן רק פרטים מקדימים 
        public async Task<string> GetGeneralCandidateInfoAsync(Candidate candidate)
        {
            if (candidate == null)
                return "פרטי המועמד אינם זמינים";

            StringBuilder generalInfo = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(candidate.StudyPlaceName))
                generalInfo.AppendLine($"שם מוסד הלימודים: {candidate.StudyPlaceName}\n");

            generalInfo.AppendLine($"גיל: {candidate.Age}\n");

            if (!string.IsNullOrWhiteSpace(candidate.City))
                generalInfo.AppendLine($"עיר: {candidate.City}\n");

            if (!string.IsNullOrWhiteSpace(candidate.Status.ToString()))
                generalInfo.AppendLine($"מצב אישי: {candidate.Status}\n");

            if (!string.IsNullOrWhiteSpace(candidate.CandidateSector.ToString()))
                generalInfo.AppendLine($"מגזר: {candidate.CandidateSector}\n");

            if (!string.IsNullOrWhiteSpace(candidate.SubSector.ToString()))
                generalInfo.AppendLine($"תת מגזר: {candidate.SubSector}\n");

            if (!string.IsNullOrWhiteSpace(candidate.Origin))
                generalInfo.AppendLine($"מוצא: {candidate.Origin}\n");

            if (!string.IsNullOrWhiteSpace(candidate.ReligiousOpenness.ToString()))
                generalInfo.AppendLine($"פתיחות דתית: {candidate.ReligiousOpenness}\n");

            if (!string.IsNullOrWhiteSpace(candidate.ClothingStyle.ToString()))
                generalInfo.AppendLine($"סגנון לבוש: {candidate.ClothingStyle}\n");

            if (!string.IsNullOrWhiteSpace(candidate.JobOrStudies.ToString()))
                generalInfo.AppendLine($"עיסוק/לימודים: {candidate.JobOrStudies}\n");

            if (!string.IsNullOrWhiteSpace(candidate.Education.ToString()))
                generalInfo.AppendLine($"מוסד לימודים: {candidate.Education}\n");

            if (candidate.Gender == Repository.Entities.Enums.Gender.נקבה)
            {
                if (!string.IsNullOrWhiteSpace(candidate.PreferredHeadCovering.ToString()))
                    generalInfo.AppendLine($"כיסוי ראש מועדף: {candidate.PreferredHeadCovering}\n");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(candidate.TorahLearning.ToString()))
                    generalInfo.AppendLine($"רמת לימוד תורה: {candidate.TorahLearning}\n");

                generalInfo.AppendLine($"רישיון נהיגה: {(candidate.License ? "כן" : "לא")}\n");
                generalInfo.AppendLine($"זקן: {(candidate.Beard ? "כן" : "לא")}\n");

                if (!string.IsNullOrWhiteSpace(candidate.SmokingStatus.ToString()))
                    generalInfo.AppendLine($"סטטוס עישון: {candidate.SmokingStatus}\n");
            }

            if (!string.IsNullOrWhiteSpace(candidate.DescriptionFind))
                generalInfo.AppendLine($"מה הוא מחפש: {candidate.DescriptionFind}\n");

            return generalInfo.ToString();
        }


        public async Task<List<CandidateDto>> GetAllByUserId(int userId)
        {
            var candidates = await repository.GetAll();
            var filteredCandidates = candidates.Where(c => c.UserId == userId).ToList();

            var result = new List<CandidateDto>();

            foreach (var candidate in filteredCandidates)
            {
                var dto = mapper.Map<CandidateDto>(candidate);

                // תמונה
                if (!string.IsNullOrEmpty(candidate.ImageUrl) && Path.GetFileName(candidate.ImageUrl) == candidate.ImageUrl)
                {
                    try
                    {
                        var imagePath = Path.Combine(Environment.CurrentDirectory, "Images", candidate.ImageUrl);
                        Console.WriteLine($"ImageUrl: {candidate.ImageUrl ?? "NULL"}");
                        if (File.Exists(imagePath))
                        {
                            dto.ArrImage = await File.ReadAllBytesAsync(imagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ שגיאה בקריאת תמונה למועמד {candidate.Id}: {ex.Message}");
                    }
                }


                // רזומה
                if (candidate.Rezumeh != null && candidate.Rezumeh.Length > 0)
                {
                    dto.RezumehArr = candidate.Rezumeh;
                }

                // אימייל וטלפון
                if (candidate.User != null)
                {
                    dto.Email = candidate.User.Email ?? "";
                    dto.PhoneNumber = candidate.User.PhoneNumber ?? "";
                }

                result.Add(dto);
            }

            return result;
        }



    }
}





