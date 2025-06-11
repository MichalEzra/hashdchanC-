using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
namespace Common.Dto
{
    public class MatchResultsDto
    {
        public CandidateDto Male { get; set; }
        public CandidateDto Female { get; set; }
        public int Score { get; set; }

    }
}
