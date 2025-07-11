using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{

    //סטטוס לשדכן
    public enum ApprovalStatus
    {
        Approved, //אושר
        PendingApproval, //בהמתנה
        Rejected  //סורב
    }

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

        public List<Candidate> Candidates { get; set; } = new List<Candidate>();
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Approved; // ברירת מחדל


    }
}
