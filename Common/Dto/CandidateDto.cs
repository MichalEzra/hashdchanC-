using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{//נתונים לתצוגה 
    public class CandidateDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender CandidateGender { get; set; } // מגדר
        public CandidateStatus Status { get; set; } // מצב אישי
        public int Age { get; set; } // גיל
        public Sector CandidateSector { get; set; } // מגזר
        public SubSector SubSector { get; set; }//תת מגזר
        public TorahStudy TorahLearning { get; set; } // לימוד תורה
        public EducationInstitution Education { get; set; } // מוסד לימודים
        public Occupation JobOrStudies { get; set; } // עיסוק
        public string City { get; set; } // עיר
        public byte[]? ArrImage { get; set; } // תמונה
        public IFormFile? fileImage { get; set; }
        public string Origin { get; set; } // מוצא
        public Language Languages { get; set; }  // שפות
        public Openness ReligiousOpenness { get; set; } // פתיחות
        public ClothingStyle ClothingStyle { get; set; }//סגנון לבוש
        public double Height { get; set; } // גובה
        public Physique Physique { get; set; }//מבנה גוף
        public SkinTone SkinTone { get; set; }//צבע עור
        public HairColor HairColor { get; set; }//צבע שיער
        public double Giving { get; set; } // כמה נותנים
        public double Expecting { get; set; } // כמה מבקשים
        public ParentsStatus FamilyStatus { get; set; } // מצב משפחתי הורים
        public bool AvailableForProposals { get; set; } // פנוי להצעות
        public HeadCovering PreferredHeadCovering { get; set; } // כיסוי ראש מועדף
        public PhoneType CandidatePhoneType { get; set; } // סוג טלפון

        public bool Beard { get; set; }//זקן
        public Smoking SmokingStatus { get; set; } // עישון
        public bool License { get; set; }//רשיון
        
    }
}
