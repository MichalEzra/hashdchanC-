using Repository.Entities;
using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IServiceMatch
    {
        Task<MatchDto> GetMatchByIdCandidates(int id1, int id2);

        Task<List<MatchDto>> GetMatchesByIdMatchmaker(int id);

        Task<List<MatchDto>> GetAllMatchByIdCandidate(int id);

        Task<List<Matchmaker>> GetAllMatchmakerActives();
    }
}
