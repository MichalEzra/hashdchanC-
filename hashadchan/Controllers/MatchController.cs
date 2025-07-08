using System.Security.Claims;
using AutoMapper;
using Common.Dto;
using Google.Cloud.Language.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using Service.Interfasces;
using Service.Services;
using Microsoft.EntityFrameworkCore;
using Service.Service;


namespace hashadchan.Controllers
{
    public class CreateMatchRequest
    {
        public int IdCandidate1 { get; set; }
        public int IdCandidate2 { get; set; }
    }

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
        private readonly IContext _context;
        public MatchController(IService<MatchDto> matchDtoService, IService<CandidateDto> candidateService, IEmailService emailService, IService<MatchmakerDto> matchmakerService,   IMapper mapper, IServiceMatch serviceMatch, IMyDetails<Candidate> candidateDetails, IContext context)
        {
            _MatchDtoService = matchDtoService;
            _candidateService = candidateService;
            _matchmakerService = matchmakerService;
            _serviceMatch = serviceMatch;
            _emailService = emailService;
            _mapper = mapper;
            _candidateDetails = candidateDetails;
            _context = context;
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
        [Authorize(Roles = "ADMIN,MATCHMAKER")] // רק מנהל או שדכן יכול לראות
        public async Task<MatchDto> Get(int id)
        {
            return await _MatchDtoService.GetById(id);
        }

        // שליפת שידוכים לפי ID של שדכן
        [HttpGet("GetMatchesByIdMatchmaker{id}")]
        [Authorize(Roles = "ADMIN,MATCHMAKER")]
        public async Task<List<MatchDto>> GetMatchesByIdMatchmaker(int id)
        {
            return await _serviceMatch.GetMatchesByIdMatchmaker(id);
        }

        //יצירת שידוך חדש
        //[HttpPost]
        //[Authorize(Roles = "MATCHMAKER")]
        //public async Task<IActionResult> Post([FromBody] CreateMatchRequest request)
        //{
        //    var idMatchmakerString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (!int.TryParse(idMatchmakerString, out int idMatchmaker))
        //        return BadRequest("Invalid user ID");

        //    var match = new Match
        //    {
        //        IdCandidateGuy = request.IdCandidate1,
        //        IdCandidateGirl = request.IdCandidate2,
        //        IdMatchmaker = idMatchmaker,
        //        Status = true,
        //        DateMatch = DateTime.Today,
        //        Active = true,
        //        IsEngaged = false,
        //        ConfirmationGirl = false,
        //        ConfirmationGuy = false
        //    };

        //    await _MatchDtoService.AddItem(_mapper.Map<MatchDto>(match));
        //    await _emailService.SendMatchEmailAsync(request.IdCandidate1, request.IdCandidate2);

        //    return Ok("Email Sent!");
        //}
        //[HttpPost]
        //[Authorize(Roles = "MATCHMAKER")]
        //public async Task<IActionResult> Post([FromBody] CreateMatchRequest request)
        //{
        //    try
        //    {
        //        // 1. שליפת מזהה המשתמש מתוך הטוקן
        //        var matchmakerUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        //        // 2. שליפת השדכן מתוך הטבלה לפי מזהה המשתמש
        //        var matchmaker = await _context.Matchmakers
        //            .FirstOrDefaultAsync(m => m.UserId == matchmakerUserId);

        //        if (matchmaker == null)
        //            return BadRequest("שדכן לא נמצא במסד הנתונים");

        //        // 3. יצירת שידוך חדש
        //        Match m = new()
        //        {
        //            IdCandidateGuy = request.IdCandidate1,
        //            IdCandidateGirl = request.IdCandidate2,
        //            IdMatchmaker = matchmaker.Id, // כאן שימוש במזהה הנכון
        //            DateMatch = DateTime.UtcNow,
        //            Status = true
        //        };

        //        await _MatchDtoService.AddItem(_mapper.Map<MatchDto>(m));
        //        await _emailService.SendMatchEmailAsync(request.IdCandidate1, request.IdCandidate2);
        //        return Ok("שידוך נוצר בהצלחה!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
        //    }
        //}
        //[HttpPost]
        //[Authorize(Roles = "MATCHMAKER,PARENT")]
        //public async Task<IActionResult> Post([FromBody] CreateMatchRequest request)
        //{
        //    try
        //    {
        //        // שליפת מזהה המשתמש מתוך הטוקן
        //        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        //        int? matchmakerId = null;

        //        // אם שדכן - נביא את ה-Id שלו
        //        if (userRole == "MATCHMAKER")
        //        {
        //            var matchmaker = await _context.Matchmakers.FirstOrDefaultAsync(m => m.UserId == userId);
        //            if (matchmaker == null)
        //                return BadRequest("שדכן לא נמצא");
        //            matchmakerId = matchmaker.Id;
        //        }

        //        // יצירת שידוך
        //        Match m = new()
        //        {
        //            IdCandidateGuy = request.IdCandidate1,
        //            IdCandidateGirl = request.IdCandidate2,
        //            IdMatchmaker = (int)matchmakerId,
        //            DateMatch = DateTime.UtcNow,
        //            Status = true
        //        };

        //        await _MatchDtoService.AddItem(_mapper.Map<MatchDto>(m));

        //        // שליחת מייל לפי תפקיד
        //        if (userRole == "MATCHMAKER")
        //        {
        //            // שלח מייל לשני הצדדים
        //            await _emailService.SendMatchEmailAsync(request.IdCandidate1, request.IdCandidate2);
        //        }
        //        else if (userRole == "CANDIDATE" || userRole == "PARENT")
        //        {
        //            // שלח רק למועמד השני (בהנחה שהיוזר הוא IdCandidate1)
        //            await _emailService.SendSingleMatchEmailAsync(request.IdCandidate1, request.IdCandidate2);
        //        }

        //        return Ok("שידוך נוצר בהצלחה!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
        //    }
        //}
        [HttpPost]
        [Authorize(Roles = "MATCHMAKER,PARENT")]
        public async Task<IActionResult> Post([FromBody] CreateMatchRequest request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                int matchmakerId;

                if (userRole == "MATCHMAKER")
                {
                    var matchmaker = await _context.Matchmakers.FirstOrDefaultAsync(m => m.UserId == userId);
                    if (matchmaker == null)
                        return BadRequest("שדכן לא נמצא");

                    matchmakerId = matchmaker.Id; // מזהה השדכן האמיתי
                }
                else
                {
                    // אם היוזר הוא לא שדכן (למשל הורה או מועמד)
                    matchmakerId = 1; // מזהה השדכן הדיפולטי שברצונך להשתמש בו
                }

                Match m = new()
                {
                    IdCandidateGuy = request.IdCandidate1,
                    IdCandidateGirl = request.IdCandidate2,
                    IdMatchmaker = matchmakerId, // תמיד יכיל ערך
                    DateMatch = DateTime.UtcNow,
                    Status = true
                };

                await _MatchDtoService.AddItem(_mapper.Map<MatchDto>(m));

                if (userRole == "MATCHMAKER")
                {
                    await _emailService.SendMatchEmailAsync(request.IdCandidate1, request.IdCandidate2);
                }
                else
                {
                    await _emailService.SendSingleMatchEmailAsync(request.IdCandidate1, request.IdCandidate2);
                }

                return Ok("שידוך נוצר בהצלחה!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
            }
        }


        // אישור שידוך דרך לינק במייל (GET עם פרמטרים candidateId ו-matchId)
        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmMatch([FromQuery] int candidateId, [FromQuery] int matchId)
        {
            var candidateDto = await _candidateService.GetById(candidateId);
            Candidate c1 = _mapper.Map<Candidate>(candidateDto);
            Match match;

            if (c1.Gender == Repository.Entities.Enums.Gender.זכר)
            {
                match = _mapper.Map<Match>(await _serviceMatch.GetMatchByIdCandidates(candidateId, matchId));
                if (match == null)
                    return NotFound("Match not found");
                if (match.ConfirmationGuy)
                    return BadRequest("כבר אישרת את ההתאמה בעבר.");
                match.ConfirmationGuy = true;
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

            await _MatchDtoService.UpdateItem(match.Id, _mapper.Map<MatchDto>(match));

            if (match.ConfirmationGuy && match.ConfirmationGirl)
            {
                var c2Dto = await _candidateService.GetById(matchId);
                Candidate c2 = _mapper.Map<Candidate>(c2Dto);
                var matchmakerDto = await _matchmakerService.GetById(match.IdMatchmaker);


                // יצירת גוף המייל המעוצב
                string c1EmailBody = await EmailTemplateHelper.CreateEmailFullDetails(_candidateDetails, c2, c1, "");
                string c2EmailBody = await EmailTemplateHelper.CreateEmailFullDetails(_candidateDetails, c1, c2, "");


                // שליחת מייל לשדכן (בלי עיצוב)
                string matchDetails = $"המועמדים אישרו את השידוך!\n\n1. {c1.FirstName} {c1.LastName}\nאימייל: {candidateDto.Email}, טלפון: {candidateDto.PhoneNumber}\n\n2. {c2.FirstName} {c2.LastName}\nאימייל: {c2Dto.Email}, טלפון: {c2Dto.PhoneNumber}";
                await _emailService.SendEmailAsync(matchmakerDto.Email, "שידוך מאושר!", matchDetails);

                // שליחת מייל לכל מועמד עם הרזומה של הצד השני (אם יש)
                await _emailService.SendEmailAsync(
                    candidateDto.Email,
                    "המשך פרטים",
                    c2EmailBody,
                    c2.Rezumeh,
                    c2.RezumehName);

                await _emailService.SendEmailAsync(
                    c2Dto.Email,
                    "המשך פרטים",
                    c1EmailBody,
                    c1.Rezumeh,
                    c1.RezumehName);

                //// שליחת מיילים עם קובץ הרזומה (אם קיים)
                //if (c2.Rezumeh != null && !string.IsNullOrEmpty(c2.RezumehName))
                //    await _emailService.SendEmailAsync(candidateDto.Email, "המשך פרטים", c2EmailBody, c2.Rezumeh, c2.RezumehName);
                //else
                //    await _emailService.SendEmailAsync(candidateDto.Email, "המשך פרטים", c2EmailBody);

                //if (c1.Rezumeh != null && !string.IsNullOrEmpty(c1.RezumehName))
                //    await _emailService.SendEmailAsync(c2Dto.Email, "המשך פרטים", c1EmailBody, c1.Rezumeh, c1.RezumehName);
                //else
                //    await _emailService.SendEmailAsync(c2Dto.Email, "המשך פרטים", c1EmailBody);

                // הודעה פשוטה לשדכן
                string matchmakerInfo = $"המועמדים {c1.FirstName} ו-{c2.FirstName} אישרו את השידוך.\n\n";
                await _emailService.SendEmailAsync(matchmakerDto.Email, "שידוך מאושר!", matchmakerInfo);

                match.Active = true;
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
        [Authorize(Roles = "ADMIN")] // רק אדמין רשאי למחוק התאמה
        public async Task<IActionResult> Delete(int id)
        {
            await _MatchDtoService.DeleteItem(id); // מחיקה
            return Ok();
        }

        [HttpGet("engaged")]
        public async Task<ActionResult<List<EngagedMatchDto>>> GetEngagedMatches()
        {
            var engaged = await _context.Matches
                .Where(m => m.IsEngaged)
                .Include(m => m.Guy)
                .Include(static m => m.Guy)
                .Include(m => m.Girl)
                .Select(m => new EngagedMatchDto
                {
                    MatchId = m.Id,
                    NameGuy = m.Guy.FirstName + " " + m.Guy.LastName,
                    NameGirl = m.Girl.FirstName + " " + m.Girl.LastName,
                    SeminaryGirl = m.Girl.StudyPlaceName,
                    YeshivaGuy = m.Guy.StudyPlaceName,
                    DateMatch = m.DateMatch
                })
                .ToListAsync();

            return Ok(engaged);
        }
    }
}
