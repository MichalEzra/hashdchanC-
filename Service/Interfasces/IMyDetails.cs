using Common.Dto;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfasces
{
    public interface IMyDetails<T>
    {
        // ממשק עבור מועמדים
        // פונקציות אסינכרוניות
        Task<T[]> GetFemaleCandidatesAsync();
        Task<T[]> GetMaleCandidatesAsync();

        // פונקציות נוספות שיכולות להיות שימושיות
        //    Task<T> GetCandidateByIdAsync(int id);
        Task<string> GetGeneralCandidateInfoAsync(Candidate candidate);
        Task<string> GetAllCandidateInfoAsync(Candidate candidate);
        Task<List<CandidateDto>> GetAllByUserId(int userId);

    }
}
