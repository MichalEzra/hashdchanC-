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

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public Gender MatchmakerGender { get; set; }

        public string? IdentityNumber { get; set; }

        public DateTime? MarriageDate { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public Sector MatchmakerSector { get; set; }

        public SubSector? SubSector { get; set; }

        public int? YearsOfExperience { get; set; }

        public int? MatchesClosed { get; set; }

        public Language Languages { get; set; }

        public Openness ReligiousOpenness { get; set; }

        public string? PhoneNumber { get; set; }
    }
}