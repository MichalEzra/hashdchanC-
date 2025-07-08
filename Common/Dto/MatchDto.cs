using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;

namespace Repository.Entities
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int IdCandidateGirl { get; set; }
        public int IdCandidateGuy { get; set; }
        public int IdMatchmaker { get; set; }
        public bool IsEngaged { get; set; } // התארסו?
        public bool Active { get; set; } = false; //  הצעה פעילה ? 
        public bool ConfirmationGuy { get; set; }
        public bool ConfirmationGirl { get; set; }
        public DateTime DateMatch { get; set; }
        public bool Status { get; set; } // ההצעה הוצעה ?
        public CandidateDto MaleCandidate { get; set; }
        public CandidateDto FemaleCandidate { get; set; }


    }

}
