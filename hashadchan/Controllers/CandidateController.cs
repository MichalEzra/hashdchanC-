using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfasces;
using Service.Services;
using System.Security.Claims;
using UserType = Common.Dto.UserType;

namespace hashadchan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IUserLinkedService<CandidateDto> candidateService;
        private readonly IService<UserDto> userService;
        private readonly IService<CandidateDto> service;
        private readonly IMyDetails<Candidate> candidateDetails;
        private readonly IRepository<Candidate> _repository;

        public CandidateController(
            IUserLinkedService<CandidateDto> candidateService,
            IService<UserDto> userService,
            IService<CandidateDto> service,
            IMyDetails<Candidate> candidateDetails,
            IRepository<Candidate> repository)  // הוסף כאן

        {
            this.candidateService = candidateService;
            this.userService = userService;
            this.service = service;
            this.candidateDetails = candidateDetails;
            _repository = repository;
        }


        //public CandidateController(IUserLinkedService<CandidateDto> candidateService,
        //                           IService<UserDto> userService,
        //                           IService<CandidateDto> service,
        //                           IMyDetails<Candidate> candidateDetails) 
        //{
        //    this.candidateService = candidateService;
        //    this.userService = userService;
        //    this.service = service;
        //    this.candidateDetails = candidateDetails;
        //}

        // החזרת כל המועמדים
        [HttpGet]
        public async Task<List<CandidateDto>> Get()
        {
            return await service.GetAll();
        }

        // החזרת מועמד לפי מזהה
        [HttpGet("{id}")]
        public async Task<CandidateDto> Get(int id)
        {
            return await service.GetById(id);
        }

        [HttpGet("{id}/general-info")]
        public async Task<ActionResult<string>> GetCandidateGeneralInfo(int id)
        {
            var candidate = await _repository.GetById(id);
            if (candidate == null)
                return NotFound("מועמד לא נמצא");

            var info = await candidateDetails.GetGeneralCandidateInfoAsync(candidate);
            return Ok(info);
        }

        // הוספת מועמד
        [Authorize(Roles = "PARENT")]
        [HttpPost]
        public async Task<ActionResult<CandidateDto>> Post([FromForm] CandidateDto candidate)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("משתמש לא מזוהה.");

            var user = await userService.GetById(userId);
            if (user == null)
                return Unauthorized("משתמש לא קיים במערכת.");

            if (user.UserType != UserType.PARENT)
                return Forbid("רק הורה יכול להוסיף מועמד.");

            //var existingCandidate = await candidateService.GetByUserId(userId);
            //if (existingCandidate != null)
            //    return BadRequest("כבר קיים מועמד המשויך ליוזר הזה.");

            candidate.UserId = userId;

            // שמירת תמונה אם קיימת
            if (candidate.fileImage != null)
            {
                candidate.ImageUrl = await UploadImage(candidate.fileImage);
            }

            // שמירת קובץ רזומה אם קיים
            if (candidate.RezumehFile != null)
            {
                candidate.RezumehName = await SaveResume(candidate.RezumehFile);
            }

            var created = await service.AddItem(candidate);
            return Ok(created);
        }

        // עדכון פרטי מועמד
        [Authorize(Roles = "PARENT")]
        [HttpPost("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] CandidateDto updatedCandidate)
        {
            await service.UpdateItem(id, updatedCandidate);
            return NoContent();
        }

        // מחיקת מועמד
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteItem(id);
            return NoContent();
        }
        //קבלת מועמדים לפי מזהה משתמש
        //[HttpGet("user/{userId}")]
        //public async Task<ActionResult<List<CandidateDto>>> GetCandidatesByUserId(int userId)
        //{
        //    var candidates = await candidateService.GetAllByUserId(userId);
        //    if (candidates == null || !candidates.Any())
        //    {
        //        return NotFound("לא נמצאו מועמדים עבור משתמש זה.");
        //    }
        //    return Ok(candidates);
        //}
        private async Task<string> UploadImage(IFormFile file)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Images", file.FileName);
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return file.FileName; // מחזירים את שם הקובץ כדי לעדכן את השדה
        }

        private async Task<string> SaveResume(IFormFile file)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Resumes", file.FileName);
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return file.FileName; // מחזירים את שם הקובץ כדי לעדכן את השדה
        }

        [HttpGet("males")]
        public async Task<IActionResult> GetMales()
        {
            return Ok(await candidateDetails.GetMaleCandidatesAsync());
        }

        [HttpGet("females")]
        public async Task<IActionResult> GetFemales()
        {
            return Ok(await candidateDetails.GetFemaleCandidatesAsync());
        }

        //[HttpGet("{id}/general-info")]
        //public async Task<ActionResult<string>> GetCandidateGeneralInfo(int id)
        //{
        //    var candidate = await _repository.GetById(id);
        //    if (candidate == null)
        //        return NotFound("מועמד לא נמצא");

        //    var info = await candidateDetails.GetGeneralCandidateInfoAsync(candidate);
        //    return Ok(info);
        //}

        [HttpGet("user/{userId}/all")]
        public async Task<ActionResult<List<CandidateDto>>> GetAllByUserId(int userId)
        {
            var candidates = await candidateService.GetAllByUserId(userId);
            if (candidates == null || !candidates.Any())
                return NotFound(); // אם לא נמצאו מועמדים

            return Ok(candidates);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<CandidateDto>>> GetCandidatesByUserId(int userId)
        {
            var candidates = await candidateDetails.GetAllByUserId(userId);

            if (candidates == null || !candidates.Any())
            {
                return NotFound("לא נמצאו מועמדים עבור משתמש זה.");
            }
            return Ok(candidates);
        }


    }
}
