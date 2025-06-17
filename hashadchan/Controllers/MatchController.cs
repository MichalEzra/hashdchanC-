using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interfaces;
using Service.Interfasces;

namespace hashadchan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController: ControllerBase
    {
        private readonly IService<CandidateDto> _candidateService;
        private readonly IService<MatchmakerDto> _matchmakerService;
        private readonly IMyDetails<Candidate> _candidateDetails;
      //  private readonly IEmailService _emailService;
        private readonly IServiceMatch _serviceMatch;
        private readonly IService<MatchDto> _MatchDtoService;
        private readonly IMapper _mapper;

        public MatchController(IService<MatchDto> matchDtoService, IService<CandidateDto> candidateService, IService<MatchmakerDto> matchmakerService,   IMapper mapper, IServiceMatch serviceMatch, IMyDetails<Candidate> candidateDetails)
        {
            _MatchDtoService = matchDtoService;
            _candidateService = candidateService;
            _matchmakerService = matchmakerService;
            _serviceMatch = serviceMatch;
           // _emailService = emailService;
            _mapper = mapper;
            _candidateDetails = candidateDetails;
        }
        // GET: api/<HistoryController>
        [HttpGet]
        //[Authorize(Roles = "admin,matchmaker")]
        [HttpGet]
        public async Task<List<MatchDto>> Get()
        {
            return await _MatchDtoService.GetAll();
        }

        // GET api/<HistoryController>/5
        //[Authorize(Roles = "admin,matchmaker")]
        [HttpGet("GetAllMatchById{id}")]
        public async Task<List<string>> GetAllMatchById(int id)
        {
            List<MatchDto> mList = await _serviceMatch.GetAllMatchByIdCandidate(id);
            List<string> sList = new List<string>();

            foreach (MatchDto match in mList)
            {
                string candidateDetails;
                var candidate = await _candidateService.GetById(id);
                if (match.ConfirmationGirl && match.ConfirmationGuy)
                {
                    candidateDetails = await _candidateDetails.GetAllCandidateInfoAsync(_mapper.Map<Candidate>(candidate));
                    sList.Add(candidateDetails);
                }
                else
                {
                    candidateDetails = await _candidateDetails.GetGeneralCandidateInfoAsync(_mapper.Map<Candidate>(candidate));
                    sList.Add(candidateDetails);
                }
            }

            return sList;
        }



        // GET api/<HistoryController>/5

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,matchmaker")]
        public async Task<MatchDto> Get(int id)
        {
            return await _MatchDtoService.GetById(id);
        }

        [HttpGet("GetMatchesByIdMatchmaker{id}")]
        [Authorize(Roles = "admin,matchmaker")]
        public async Task<List<MatchDto>> GetMatchesByIdMatchmaker(int id)
        {
            return await _serviceMatch.GetMatchesByIdMatchmaker(id);
        }

        // POST api/<HistoryController>
        //[Authorize(Roles = "admin,matchmaker")]
        [HttpPost]
        public async Task<IActionResult> Post(int idCandudate1, int idCandudate2, int idMatchmaker)
        {
            Match m = new()
            {
                IdCandidateGuy = idCandudate1,
                IdCandidateGirl = idCandudate2,
                IdMatchmaker = idMatchmaker,
                Status = true
            };

            await _MatchDtoService.AddItem(_mapper.Map<MatchDto>(m));
           // await _emailService.SendMatchEmailAsync(idCandudate1, idCandudate2);
            return Ok("Email Sent!");
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmMatch([FromQuery] int candidateId, [FromQuery] int matchId)
        {
            var candidateDto = await _candidateService.GetById(candidateId);
            Candidate c1 = _mapper.Map<Candidate>(candidateDto);
            Match match;

            if (c1.CandidateGender ==Repository.Entities.Enums.Gender.MALE)
            {
                match = _mapper.Map<Match>(await _serviceMatch.GetMatchByIdCandidates(candidateId, matchId));
                if (match == null)
                    return NotFound("Match not found");

                if (match.ConfirmationGuy)
                    return BadRequest("כבר אישרת את ההתאמה בעבר. לא ניתן לאשר שוב.");

                match.ConfirmationGuy = true;
            }
            else
            {
                match = _mapper.Map<Match>(await _serviceMatch.GetMatchByIdCandidates(matchId, candidateId));
                if (match == null)
                    return NotFound("Match not found");

                if (match.ConfirmationGirl)
                    return BadRequest("כבר אישרת את ההתאמה בעבר. לא ניתן לאשר שוב.");

                match.ConfirmationGirl = true;
            }

            await _MatchDtoService.UpdateItem(match.Id, _mapper.Map<MatchDto>(match));

            if (match.ConfirmationGuy && match.ConfirmationGirl)
            {
                var c2Dto = await _candidateService.GetById(matchId);
                Candidate c2 = _mapper.Map<Candidate>(c2Dto);
                var matchmakerDto = await _matchmakerService.GetById(match.IdMatchmaker);
                Matchmaker matchmaker = _mapper.Map<Matchmaker>(matchmakerDto);

                //string matchDetails = $"המועמדים אישרו את השידוך!\n" +
                //    $"פרטים:\n1. {_candidateDetails.GetAllCandidateInfo(c1)}\nאימייל: {c1.Email}, טלפון: {c1.Phone}\n\n" +
                //    $"2. {_candidateDetails.GetAllCandidateInfo(c2)}\nאימייל: {c2.Email}, טלפון: {c2.Phone}";

                //await _emailService.SendEmailAsync(matchmaker.Email, "שידוך מאושר!", matchDetails);
                //await _emailService.SendEmailAsync(c1.Email, "המשך פרטים", _candidateDetails.GetAllCandidateInfo(c2));
                //await _emailService.SendEmailAsync(c2.Email, "המשך פרטים", _candidateDetails.GetAllCandidateInfo(c1));

                match.Active = true;
                await _MatchDtoService.UpdateItem(match.Id, _mapper.Map<MatchDto>(match));
            }

            return Ok("Match confirmed!");
        }

        //[Authorize(Roles = "admin,matchmaker")]
        //לבדוק את העניין שאסור ששדכן יוכל לשנות סתם דברים אולי צריך להוסיף כאן עוד פונקציה שמשנה רק משהו ספציפי ואת הפעולה של עדכון ההיסטוריה להרשות רק למנהל
        // PUT api/<HistoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MatchDto value)
        {
            await _MatchDtoService.UpdateItem(id, value);
            return Ok();
        }

        // DELETE api/<HistoryController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _MatchDtoService.DeleteItem(id);
            return Ok();
        }
    }
}
