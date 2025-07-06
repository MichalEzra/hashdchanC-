using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Entities.Enums;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CandidateRepository : IRepository<Candidate>
    {
        private readonly IContext context;
        public CandidateRepository(IContext context)
        {
            this.context = context;
        }
        public async Task<Candidate> AddItem(Candidate item)
        {
            await this.context.Candidates.AddAsync(item);
            await this.context.Save();
            return item;
        }


        public async Task DeleteItem(int id)
        {
            var candidate =await GetById(id);
            if (candidate == null)
            {
                throw new Exception("Candidate not found");
            }
            this.context.Candidates.Remove(candidate);
            await this.context.Save();
        }

        public async Task<List<Candidate>> GetAll()
        {
            return await context.Candidates.ToListAsync();
        }

        public async Task<Candidate> GetById(int id)
        {
            return await context.Candidates.Include(c => c.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(int id, Candidate item)
        {
            var candidate = await GetById(id);
            if (candidate == null)
            {
                throw new Exception("Candidate not found");
            }
            candidate.UserId = item.UserId;
            candidate.CandidateId = item.CandidateId;
            candidate.FirstName = item.FirstName;
            candidate.LastName = item.LastName;
            candidate.Gender = item.Gender;
            candidate.Status = item.Status;
            candidate.Age = item.Age;
            candidate.CandidateSector = item.CandidateSector;
            candidate.SubSector = item.SubSector; // תת מגזר
            candidate.TorahLearning = item.TorahLearning; // לימוד תורה
            candidate.Education = item.Education; // מוסד לימודים
            candidate.StudyPlaceName = item.StudyPlaceName; //שם מובד הלימודים
            candidate.JobOrStudies = item.JobOrStudies; // עיסוק
            candidate.City = item.City; // עיר
            candidate.ImageUrl = item.ImageUrl; // תמונה
            candidate.Origin = item.Origin; // מוצא
            candidate.Languages = item.Languages; // שפות
            candidate.ReligiousOpenness = item.ReligiousOpenness; // פתיחות דתית
            candidate.ClothingStyle = item.ClothingStyle; // סגנון לבוש
            candidate.Height = item.Height; // גובה
            candidate.Physique = item.Physique; // מבנה גוף
            candidate.SkinTone = item.SkinTone; // צבע עור
            candidate.HairColor = item.HairColor; // צבע שיער
            candidate.Giving = item.Giving; // כמה נותנים
            candidate.Expecting = item.Expecting; // כמה מבקשים
            candidate.FamilyStatus = item.FamilyStatus; // מצב משפחתי
            candidate.AvailableForProposals = item.AvailableForProposals; // פנוי להצעות
            candidate.PreferredHeadCovering = item.PreferredHeadCovering; // כיסוי ראש מועדף
            candidate.CandidatePhoneType = item.CandidatePhoneType; // סוג טלפון
            candidate.Beard = item.Beard; // זקן
            candidate.SmokingStatus = item.SmokingStatus; // עישון
            candidate.License = item.License; // רישיון
            candidate.RezumehName = item.RezumehName;
            candidate.Rezumeh = item.Rezumeh;
            candidate.DescriptionFind = item.DescriptionFind;
            candidate.DescriptionSelf = item.DescriptionSelf;
            await context.Save();
        }
        public async Task<Candidate[]> GetFemaleCandidatesAsync()
        {
            return await context.Candidates
                .Where(c => c.Gender == Gender.נקבה && c.AvailableForProposals)
                .ToArrayAsync();
        }

        public async Task<Candidate[]> GetMaleCandidatesAsync()
        {
            return await context.Candidates
                .Where(c => c.Gender == Gender.זכר && c.AvailableForProposals)
                .ToArrayAsync();
        }

        public async Task<Candidate> GetCandidateByIdAsync(int id)
        {
            return await context.Candidates.FindAsync(id);
        }

        public async Task<Candidate[]> GetAllCandidatesAsync()
        {
            return await context.Candidates
                .Where(c => c.AvailableForProposals)
                .ToArrayAsync();
        }

        public async Task<int> GetCandidatesCountAsync(Gender gender)
        {
            return await context.Candidates
                .CountAsync(c => c.Gender == gender && c.AvailableForProposals);
        }
    }
}

