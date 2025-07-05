using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int IdCandidateGirl { get; set; }
        public int IdCandidateGuy { get; set; }
        public int IdMatchmaker { get; set; }
        public bool IsEngaged { get; set; } = false; // האם יתארסו
        public DateTime DateMatch { get; set; } = DateTime.Today; // תאריך שינוי אחרון 
        public bool Status { get; set; } = false; // האם הצעה הוצעה או לא 
        public bool Active { get; set; } = false; // האם הצעה פעילה או לא 
        public bool ConfirmationGirl { get; set; } = false; // האם הבחורה אישרה את ההצעה 
        public bool ConfirmationGuy { get; set; } = false; // האם הבחור אישר את ההצעה 
        [ForeignKey("IdCandidateGirl")]
        public Candidate Girl { get; set; }
        [ForeignKey("IdCandidateGuy")]
        public Candidate Guy { get; set; }
        [ForeignKey("IdMatchmaker")]
        public Matchmaker Matchmaker { get; set; }

    }

}
