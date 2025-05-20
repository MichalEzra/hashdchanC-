using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public enum UserType
    {
        ADMIN, MATCHMAKER, PARENT
    }
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? PhoneNumber { get; set; }  // הטלפון של היוזר עצמו

        public UserType UserType { get; set; }

        public Candidate? Candidate { get; set; }  // למקרה שהיוזר הוא הורה ויש לו מועמד אחד
    }
}
