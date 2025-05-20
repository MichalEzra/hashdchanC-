using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
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
        public string? ImageUrl { get; set; } // תמונה
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
        //public virtual User user { get; set; }
        ////פרטי משפחה
        //public FamilyStyle FamilyStyle { get; set; }//סגנון משפחה

        //public FamilyOpenness FamilyOpenness { get; set; }//רמת פתיחות
        //public string FatherName { get; set; }
        //public string FatherOccupation { get; set; }
        //public string MotherName { get; set; }
        //public string NameFromHome { get; set; }
        //public string MotherOccupation { get; set; }
        ////אחים ואחיות
        //public List<Brother> Brothers { get; set; }
        //public string DescriptionFind { get; set; }
        //public List<Inquiries> Inquiries { get; set; }//טלפונים לבירורים
        //public string ImageUrl { get; set; }
        //public bool Status { get; set; }

    }
}
