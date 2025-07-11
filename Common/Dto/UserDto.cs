using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public enum UserType
    {
        ADMIN, MATCHMAKER, PARENT
    }

    public class UserDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? PhoneNumber { get; set; }

        public UserType UserType { get; set; }
        public List<CandidateDto> Candidates { get; set; } = new List<CandidateDto>();
        public bool IsApproved { get; set; }


    }

}