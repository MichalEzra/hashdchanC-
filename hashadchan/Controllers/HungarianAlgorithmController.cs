using AutoMapper;
using HungarianAlgorithm;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Common.Dto;
using Service.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HungarianAlgorithmController : ControllerBase
    {
        private readonly IHungarianAlgorithm _matchingService;
        private readonly IMapper _mapper;

        public HungarianAlgorithmController(IHungarianAlgorithm matchingService, IMapper mapper)
        {
            _matchingService = matchingService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<CandidateDto[][]>> Get()
        {
            await _matchingService.InitializeCandidatesAsync();

            _matchingService.MatrixFilling(_matchingService.CostMatrix);

            if (_matchingService.CostMatrix == null)
                return StatusCode(500, "CostMatrix is not initialized.");

            (Candidate[,] idAssignments, int[] costMatch) = _matchingService.RunHungarianAlgorithm(_matchingService.CostMatrix);

            int rows = idAssignments.GetLength(0);
            int cols = idAssignments.GetLength(1);

            CandidateDto[][] jaggedArray = new CandidateDto[rows][];
            for (int i = 0; i < rows; i++)
            {
                jaggedArray[i] = new CandidateDto[cols];
                for (int j = 0; j < cols; j++)
                {
                    jaggedArray[i][j] = _mapper.Map<CandidateDto>(idAssignments[i, j]);
                }
            }

            return Ok(jaggedArray);
        }

        [HttpGet("male")]
        public async Task<ActionResult<List<MatchResultsDto>>> Get10Male()
        {
            await _matchingService.InitializeCandidatesAsync();
            _matchingService.MatrixFilling(_matchingService.CostMatrixMale);

            if (_matchingService.CostMatrixMale == null)
                return StatusCode(500, "CostMatrix is not initialized.");

            (Candidate[,] idAssignments, int[] costMatch) = _matchingService.RunHungarianAlgorithm(_matchingService.CostMatrixMale);
            List<MatchResultsDto> matchResults = new();

            for (int i = 0; i < idAssignments.GetLength(0); i++)
            {
                matchResults.Add(new MatchResultsDto
                {
                    Male = _mapper.Map<CandidateDto>(idAssignments[i, 0]),
                    Female = _mapper.Map<CandidateDto>(idAssignments[i, 1]),
                    Score = costMatch[i]
                });
            }

            return Ok(matchResults);
        }

        [HttpGet("female")]
        public async Task<ActionResult<List<MatchResultsDto>>> Get10Female()
        {
            await _matchingService.InitializeCandidatesAsync();
            _matchingService.MatrixFilling(_matchingService.CostMatrixFemale);

            if (_matchingService.CostMatrixFemale == null)
                return StatusCode(500, "CostMatrix is not initialized.");

            (Candidate[,] idAssignments, int[] costMatch) = _matchingService.RunHungarianAlgorithm(_matchingService.CostMatrixFemale);
            List<MatchResultsDto> matchResults = new();

            for (int i = 0; i < idAssignments.GetLength(0); i++)
            {
                matchResults.Add(new MatchResultsDto
                {
                    Female = _mapper.Map<CandidateDto>(idAssignments[i, 0]),
                    Male = _mapper.Map<CandidateDto>(idAssignments[i, 1]),
                    Score = costMatch[i]
                });
            }

            return Ok(matchResults);
        }
    }
}
