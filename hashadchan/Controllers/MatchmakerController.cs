using Common.Dto;
using hashadchan.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfasces;
using Service.Services;
using System.Security.Claims;


namespace hashadchan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchmakerController : ControllerBase
    {
        private readonly IUserLinkedService<MatchmakerDto> matchmakerService;
        private readonly IService<UserDto> service;
        private readonly IService<MatchmakerDto> mService;
        public MatchmakerController(IUserLinkedService<MatchmakerDto> matchmakerService,IService<UserDto> service,IService<MatchmakerDto> mService)
        {
            this.matchmakerService = matchmakerService;
            this.service = service;
            this.mService = mService;
        }

        // החזרת כל השדכנים
        [HttpGet]
        public async Task<List<MatchmakerDto>> Get()
        {
            return await mService.GetAll();
        }

        // החזרת שדכן ספציפי לפי ID
        [HttpGet("{id}")]
        public async Task<MatchmakerDto> Get(int id)
        {
            return await mService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<MatchmakerDto>> Post([FromForm] MatchmakerDto matchmaker)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("משתמש לא מזוהה.");

            var user = await service.GetById(userId);
            if (user == null)
                return Unauthorized("משתמש לא קיים במערכת.");

            if (user.UserType != UserType.MATCHMAKER)
                return Forbid("רק שדכנים רשאים להירשם כשדכנים.");

            var existing = await matchmakerService.GetAllByUserId(userId);
            if (existing != null)
                return BadRequest("כבר קיים שדכן עבור משתמש זה.");

            matchmaker.UserId = userId;

            var created = await mService.AddItem(matchmaker);
            return Ok(created);
        }
        [Authorize(Roles = "MATCHMAKER")]
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] MatchmakerDto updatedMatchmaker)
        {
            await mService.UpdateItem(id, updatedMatchmaker); // עדכון פרטי שדכן
        }
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await mService.DeleteItem(id); // מחיקת שדכן
        }
    }
}

