using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Matchmaker
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
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

       
        public string PhoneNumber { get; set; } // מספר טלפון
    }
}

