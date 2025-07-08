using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Common.Dto
{
    public class CandidateDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int CandidateId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public CandidateStatus Status { get; set; }

        public int Age { get; set; }

        public Sector CandidateSector { get; set; }

        public SubSector SubSector { get; set; }

        public TorahStudy TorahLearning { get; set; }

        public EducationInstitution Education { get; set; }
        public string? StudyPlaceName { get; set; }  // שם מוסד הלימודים

        public Occupation JobOrStudies { get; set; }

        public string City { get; set; }

        public byte[]? ArrImage { get; set; } // תמונה
        public IFormFile? fileImage { get; set; }
        public string? ImageUrl { get; set; }
        public string Origin { get; set; }

        public Language Languages { get; set; }

        public Openness ReligiousOpenness { get; set; }

        public ClothingStyle ClothingStyle { get; set; }

        public double Height { get; set; }

        public Physique Physique { get; set; }

        public SkinTone SkinTone { get; set; }

        public HairColor HairColor { get; set; }

        public double Giving { get; set; }

        public double Expecting { get; set; }

        public ParentsStatus FamilyStatus { get; set; }

        public bool AvailableForProposals { get; set; }

        public HeadCovering PreferredHeadCovering { get; set; }

        public PhoneType CandidatePhoneType { get; set; }

        public bool Beard { get; set; }

        public Smoking SmokingStatus { get; set; }

        public bool License { get; set; }
        public byte[]? RezumehArr { get; set; }         // רזומה - תוכן בבייטים
        public IFormFile? RezumehFile { get; set; }     // רזומה - קובץ מהקליינט
        public string? RezumehName { get; set; }        // רזומה - שם קובץ
        public string DescriptionSelf { get; set; } // תיאור עצמי של המועמד
        public string DescriptionFind { get; set; } // תיאור של מה שהוא מחפש 
    }
}

