using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfasces;
using System.Security.Claims;

namespace hashadchan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IUserLinkedService<CandidateDto> candidateService;
        private readonly IService<UserDto> userService;
        private readonly IService<CandidateDto> service;

        public CandidateController(IUserLinkedService<CandidateDto> candidateService,
                                   IService<UserDto> userService,
                                   IService<CandidateDto> service)
        {
            this.candidateService = candidateService;
            this.userService = userService;
            this.service = service;
        }

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

        // הוספת מועמד

        [HttpPost]
        public async Task<ActionResult<CandidateDto>> Post([FromForm] CandidateDto candidate)
        {
            // שליפת מזהה המשתמש המחובר מה-JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("משתמש לא מזוהה.");

            // שליפת המשתמש מהמערכת
            var user = await userService.GetById(userId);
            if (user == null)
                return Unauthorized("משתמש לא קיים במערכת.");

            // רק משתמש שהוא הורה יכול להוסיף מועמד
            if (user.UserType != UserType.PARENT)
                return Forbid("רק הורה יכול להוסיף מועמד.");

            // בדיקה אם כבר קיים מועמד למשתמש הזה
            var existingCandidate = await candidateService.GetByUserId(userId);
            if (existingCandidate != null)
                return BadRequest("כבר קיים מועמד המשויך ליוזר הזה.");

            // שיוך המועמד ליוזר
            candidate.UserId = userId;

            // שמירת תמונה אם קיימת
            if (candidate.fileImage != null)
            {
                UploadImage(candidate.fileImage);
                var imageFileName = candidate.fileImage?.FileName;
            }

            // הוספה למערכת
            var created = await service.AddItem(candidate);
            return Ok(created);
        }

        // עדכון פרטי מועמד
        [Authorize(Roles = "PARENT")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CandidateDto updatedCandidate)
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

        private void UploadImage(IFormFile file)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Images/", file.FileName);
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
    }
}
