using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Service.Interfasces;
using Service.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserType = Common.Dto.UserType;

namespace hashadchan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<UserDto> service;
        private readonly IUserService<User> userService;
        private readonly IConfiguration config;
        private readonly IEmailService _emailService;


        public UserController(IService<UserDto> service, IConfiguration config, IEmailService emailService)
        {
            this.service = service;
            this.config = config;
            _emailService = emailService;
        }
        // החזרת כל המשתמשים
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<List<UserDto>> Get()
        {
            return await service.GetAll();
        }


        // החזרת משתמש ספציפי לפי ID
        [HttpGet("{id}")]
        public async Task<UserDto> Get(int id)
        {
            return await service.GetById(id);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto user)
        {
            // בדיקה שהערך שנשלח הוא Enum חוקי
            if (!Enum.IsDefined(typeof(UserType), user.UserType))
            {
                return BadRequest("סוג המשתמש אינו חוקי.");
            }

            // מניעת יצירת משתמש מסוג ADMIN
            if (user.UserType == UserType.ADMIN)
            {
                return Unauthorized("לא ניתן להוסיף מנהלים דרך המערכת.");
            }

            if (user.UserType == UserType.MATCHMAKER)
            {
                // סימון השדכן כממתין לאישור מנהל
                user.IsApproved = false; // ודא ששדה זה קיים ב-UserDto וב-Entity שלך

                var addedUser = await service.AddItem(user);

                // שליחת מייל למנהל לאישור השדכן
                await _emailService.SendEmailToAdminForMatchmakerApproval(
                    addedUser.FullName,
                    addedUser.Email,
                    addedUser.PhoneNumber,
                    addedUser.UserType
                );

                return Ok(new { message = "הרשמתך התקבלה וממתינה לאישור מנהל." });
            }
            else
            {
                // טיפול במשתמשים מסוגים אחרים - פשוט שמור ותחזיר
                var addedUser = await service.AddItem(user);
                return Ok(addedUser);
            }
        }

        [HttpGet("approve-matchmaker")]
        public async Task<IActionResult> ApproveMatchmaker([FromQuery] string email)
        {
            var user = await userService.GetByEmail(email); // תוודאי שיש פונקציה כזו

            if (user == null)
                return NotFound("שדכן לא נמצא");

            if (user.IsApproved)
                return BadRequest("השדכן כבר אושר");

            user.IsApproved = true;
            await service.UpdateItem(user.Id, user); // או קריאה לפונקציית עדכון מתאימה

            // שליחת מייל לשדכן
            await _emailService.SendApprovalEmailToMatchmaker(user.FullName, user.Email);

            // החזרת הודעה למשתמש
            return Content("<h2 style='font-family:sans-serif;color:green;'>✔️ השדכן אושר בהצלחה</h2>", "text/html");
        }

        [HttpPost("login")]
        public async Task<string> Login([FromBody] UserLogin value)
        {
            var user = await Authenticate(value);
            if (user != null)
            {
                var token = Generate(user);
                return token;
            }
            return "user not found";
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UserDto updatedUser)
        {
            await service.UpdateItem(id, updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItem(id); // ← חשוב!
        }

        //פונקציה לאישור מנהל
        //[Authorize(Roles = "Admin")]
        //[HttpPut("approve/{id}")]
        //public async Task<IActionResult> ApproveMatchmaker(int id)
        //{
        //    var user = await service.GetById(id);
        //    if (user == null) return NotFound();

        //    user.UserType = UserType.Matchmaker;
        //    user.ApprovalStatus = ApprovalStatus.Approved;
        //    await _dbContext.SaveChangesAsync();

        //    return Ok("השדכן אושר בהצלחה");
        //}
        [HttpGet("type/{userType}")]
        public async Task<ActionResult<List<UserDto>>> GetUsersByType(UserType userType)
        {
            var users = await userService.GetUsersByType(userType);
            return Ok(users);
        }


        private async Task<UserDto> Authenticate(UserLogin value)
        {
            // נניח ש-service.GetAll() מחזיר Task<List<UserDto>> או שהוא עטוף כך:
            var users = await service.GetAll();
            UserDto user = users.FirstOrDefault(x => x.Password == value.Password && x.Email == value.Email);
            if (user != null)
                return user;
            return null;
        }
        private string Generate(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Role, user.UserType.ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // ✅ חשוב!
    };

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
    claims,
    expires: DateTime.Now.AddHours(15),
    signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }



}
