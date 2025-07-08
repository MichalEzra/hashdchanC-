using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class EngagedMatchDto
    {
        public int MatchId { get; set; }
        public string NameGuy { get; set; }
        public string NameGirl { get; set; }
        public string YeshivaGuy { get; set; }
        public string SeminaryGirl { get; set; }
        public string CityGuy { get; set; }
        public string CityGirl { get; set; }
        public DateTime DateMatch { get; set; }
    }

}
