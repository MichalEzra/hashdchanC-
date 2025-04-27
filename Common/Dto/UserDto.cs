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
        PARENT, CANDIDATE, MATCHMAKER
    }
    public class UserDto
    {
        [Key] //  מפתח ראשי
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public string? ContactPersonFirstName { get; set; }
        public string? ContactPersonLastName { get; set; }
        public string? ContactPersonPhone { get; set; }
    }
}