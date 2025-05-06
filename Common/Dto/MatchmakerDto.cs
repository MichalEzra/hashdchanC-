using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class MatchmakerDto
    {
        public int Id { get; set; }
        public int MatchmakerId { get; set; }
        public int UserId { get; set; }

        public string FirstName { get; set; } // שם פרטי
        public string LastName { get; set; } // שם משפחה
        public DateTime BirthDate { get; set; } // תאריך לידה
        public Gender MatchmakerGender { get; set; } // מגדר
        public string IdentityNumber { get; set; } // מספר זהות
        public DateTime? MarriageDate { get; set; } // תאריך נישואין
        public string? Country { get; set; } // מדינה
        public string? City { get; set; } // עיר
        public Sector MatchmakerSector { get; set; } // מגזר
        public string? SubSector { get; set; } // תת מגזר
        public int? YearsOfExperience { get; set; } // שנות ותק בשדכנות
        public int? MatchesClosed { get; set; } // מספר שידוכים שסגרתי
        public Language Languages { get; set; }  // שפות
        public Openness ReligiousOpenness { get; set; } // פתיחות

        // פרטי בנק
        public string? BankName { get; set; } // בנק
        public string? BranchNumber { get; set; } // מספר סניף
        public string? AccountNumber { get; set; } // מספר חשבון
        public string? AccountName { get; set; } // שם החשבון

        // המלצות
        public string? RecommendedMatchmaker1 { get; set; } // שדכן ממליץ 1
        public string? RecommendedMatchmaker2 { get; set; } // שדכן ממליץ 2

        // דמי שידוך
        public double? MatchFeeFirstMarriage { get; set; } // דמי שידוך פרק א'
        public double? MatchFeeSecondMarriage { get; set; } // דמי שידוך פרק ב'
        public double? MatchFeeAbove30 { get; set; } // דמי שידוך לבני 30+

        public string PhoneNumber { get; set; } // מספר טלפון

    }
}
