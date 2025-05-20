using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interfasces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hashadchan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<UserDto> service;
        private readonly IConfiguration config;

        public UserController(IService<UserDto> service, IConfiguration config)
        {
            this.service = service;
            this.config = config;
        }
        // החזרת כל המשתמשים
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

        //[Authorize] 
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] UserDto user)
        //{
        //    // שליפת סוג המשתמש מתוך הטוקן
        //    var userTypeFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        //    if (user.UserType.ToString() == "Admin" && userTypeFromToken != "Admin")
        //    {
        //        return Forbid("Only an Admin can create another Admin");
        //    }

        //    var newUser = await service.AddItem(user);
        //    return Ok(newUser);
        //}
        // [Authorize]   ← לא נשים את זה
        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserDto user)
        {
            if (user.UserType == UserType.MATCHMAKER)
            {
                var userTypeClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(userTypeClaim) || userTypeClaim != "ADMIN")
                {
                    return Unauthorized("רק מנהל רשאי להוסיף שדכן.");
                }
            }

            if (user.UserType == UserType.ADMIN)
            {
                return Unauthorized("לא ניתן להוסיף מנהלים דרך המערכת.");
            }

            var newUser = await service.AddItem(user);
            return Ok(newUser);
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
        new Claim(ClaimTypes.Role, user.UserType.ToString()) // מוסיפים את התפקיד
    };

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
