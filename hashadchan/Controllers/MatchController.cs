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
        private readonly IEmailService _emailService;
        private readonly IServiceMatch _serviceMatch;
        private readonly IService<MatchDto> _MatchDtoService;
        private readonly IMapper _mapper;

        public MatchController(IService<MatchDto> matchDtoService, IService<CandidateDto> candidateService, IEmailService emailService, IService<MatchmakerDto> matchmakerService,   IMapper mapper, IServiceMatch serviceMatch, IMyDetails<Candidate> candidateDetails)
        {
            _MatchDtoService = matchDtoService;
            _candidateService = candidateService;
            _matchmakerService = matchmakerService;
            _serviceMatch = serviceMatch;
            _emailService = emailService;
            _mapper = mapper;
            _candidateDetails = candidateDetails;
        }

        // שליפת כל השידוכים הקיימים
        [HttpGet]
        public async Task<List<MatchDto>> Get()
        {
            return await _MatchDtoService.GetAll(); // מחזיר את כל ההתאמות
        }

        // שליפת רשימת התאמות לפי ID של מועמד
        [HttpGet("GetAllMatchById{id}")]
        public async Task<List<string>> GetAllMatchById(int id)
        {
            List<MatchDto> mList = await _serviceMatch.GetAllMatchByIdCandidate(id); // שליפת התאמות
            List<string> sList = new List<string>(); // יצירת רשימה לתצוגת פרטים

            foreach (MatchDto match in mList) // מעבר על כל שידוך
            {
                string candidateDetails;
                var candidate = await _candidateService.GetById(id); // שליפת מועמד
                if (match.ConfirmationGirl && match.ConfirmationGuy) // אם שני הצדדים אישרו
                {
                    candidateDetails = await _candidateDetails.GetAllCandidateInfoAsync(_mapper.Map<Candidate>(candidate)); // פרטים מלאים
                    sList.Add(candidateDetails);
                }
                else
                {
                    candidateDetails = await _candidateDetails.GetGeneralCandidateInfoAsync(_mapper.Map<Candidate>(candidate)); // פרטים כלליים
                    sList.Add(candidateDetails);
                }
            }

            return sList;
        }

        // שליפה לפי ID של שידוך מסוים
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,matchmaker")] // רק מנהל או שדכן יכול לראות
        public async Task<MatchDto> Get(int id)
        {
            return await _MatchDtoService.GetById(id);
        }

        // שליפת שידוכים לפי ID של שדכן
        [HttpGet("GetMatchesByIdMatchmaker{id}")]
        [Authorize(Roles = "admin,matchmaker")]
        public async Task<List<MatchDto>> GetMatchesByIdMatchmaker(int id)
        {
            return await _serviceMatch.GetMatchesByIdMatchmaker(id);
        }

        // יצירת שידוך חדש
        [HttpPost]
        public async Task<IActionResult> Post(int idCandudate1, int idCandudate2, int idMatchmaker)
        {
            Match m = new() // יצירת אובייקט חדש
            {
                IdCandidateGuy = idCandudate1,
                IdCandidateGirl = idCandudate2,
                IdMatchmaker = idMatchmaker,
                Status = true // מצב פעיל
            };

            await _MatchDtoService.AddItem(_mapper.Map<MatchDto>(m)); // הוספה למסד
            // await _emailService.SendMatchEmailAsync(idCandudate1, idCandudate2); // שליחת מייל (כרגע מבוטל)
            return Ok("Email Sent!");
        }

        // אישור שידוך דרך לינק במייל
        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmMatch([FromQuery] int candidateId, [FromQuery] int matchId)
        {
            var candidateDto = await _candidateService.GetById(candidateId);
            Candidate c1 = _mapper.Map<Candidate>(candidateDto);
            Match match;

            if (c1.CandidateGender == Repository.Entities.Enums.Gender.MALE) // אם מועמד הוא גבר
            {
                match = _mapper.Map<Match>(await _serviceMatch.GetMatchByIdCandidates(candidateId, matchId));
                if (match == null)
                    return NotFound("Match not found");

                if (match.ConfirmationGuy)
                    return BadRequest("כבר אישרת את ההתאמה בעבר.");

                match.ConfirmationGuy = true; // סימון כאילו אישר
            }
            else
            {
                match = _mapper.Map<Match>(await _serviceMatch.GetMatchByIdCandidates(matchId, candidateId));
                if (match == null)
                    return NotFound("Match not found");

                if (match.ConfirmationGirl)
                    return BadRequest("כבר אישרת את ההתאמה בעבר.");

                match.ConfirmationGirl = true;
            }

            await _MatchDtoService.UpdateItem(match.Id, _mapper.Map<MatchDto>(match)); // עדכון ההתאמה

            if (match.ConfirmationGuy && match.ConfirmationGirl) // אם שניהם אישרו
            {
                var c2Dto = await _candidateService.GetById(matchId);
                Candidate c2 = _mapper.Map<Candidate>(c2Dto);
                var matchmakerDto = await _matchmakerService.GetById(match.IdMatchmaker);
                Matchmaker matchmaker = _mapper.Map<Matchmaker>(matchmakerDto);

                // יצירת טקסט עם פרטי שני המועמדים
                string matchDetails = $"המועמדים אישרו את השידוך!\n" +
                    $"פרטים:\n1. {_candidateDetails.GetAllCandidateInfoAsync(c1)}\nאימייל: {c1.User.Email}, טלפון: {c1.User.PhoneNumber}\n\n" +
                    $"2. {_candidateDetails.GetAllCandidateInfoAsync(c2)}\nאימייל: {c2.User.Email}, טלפון: {c2.User.PhoneNumber}";

                // שליחת מיילים לשני הצדדים ולשדכן
                await _emailService.SendEmailAsync(matchmaker.User.Email, "שידוך מאושר!", matchDetails);
                await _emailService.SendEmailAsync(c1.User.Email, "המשך פרטים", await _candidateDetails.GetAllCandidateInfoAsync(c2));
                await _emailService.SendEmailAsync(c2.User.Email, "המשך פרטים", await _candidateDetails.GetAllCandidateInfoAsync(c1));

                match.Active = true; // הפיכת ההתאמה לפעילה
                await _MatchDtoService.UpdateItem(match.Id, _mapper.Map<MatchDto>(match));
            }

            return Ok("Match confirmed!");
        }

        // עדכון שידוך
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MatchDto value)
        {
            await _MatchDtoService.UpdateItem(id, value); // עדכון במסד
            return Ok();
        }

        // מחיקת שידוך לפי ID
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // רק אדמין רשאי למחוק התאמה
        public async Task<IActionResult> Delete(int id)
        {
            await _MatchDtoService.DeleteItem(id); // מחיקה
            return Ok();
        }
    }
}
