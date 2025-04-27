using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfasces;

namespace hashadchan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<UserDto> service;

        public UserController(IService<UserDto> service)
        {
            this.service = service;
        }

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // החזרת כל המשתמשים
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            var users = service.GetAll(); // קרא לכל המשתמשים
            return Ok(users); // מחזיר את המשתמשים בתגובה עם קוד סטטוס 200
        }

        //[HttpGet("{id}")]
        //public UserDto Get(int id)
        //{
        //    return service.GetById(id);
        //}
        // החזרת משתמש ספציפי לפי ID
        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(int id)
        {
            var user = service.GetById(id);
            if (user == null)
            {
                return NotFound(); // אם המשתמש לא נמצא, מחזיר 404
            }
            return Ok(user);
        }


        [HttpPost]
        public UserDto Post([FromBody] UserDto user)
        {
            return service.AddItem(user); // הוספת המשתמש
        }
        //// הוספת משתמש חדש
        //[HttpPost]
        //public ActionResult<UserDto> Post([FromBody] UserDto user)
        //{
        //    var createdUser = service.AddItem(user); // הוספת המשתמש
        //    return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser); // מחזיר את המשתמש שנוסף
        //}

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserDto updatedUser)
        {
            service.UpdateItem(id, updatedUser); // עדכון פרטי המשתמש
        }
        //// עדכון פרטי משתמש קיים
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] UserDto updatedUser)
        //{
        //    var existingUser = service.GetById(id);
        //    if (existingUser == null)
        //    {
        //        return NotFound(); // אם המשתמש לא נמצא, מחזיר 404
        //    }

        //    service.UpdateItem(id, updatedUser); // עדכון פרטי המשתמש
        //    return NoContent(); // מחזיר 204 (לא משנה את תוכן התגובה)
        //}


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id); // מחיקת משתמש
        }
        //// מחיקת משתמש
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var existingUser = service.GetById(id);
        //    if (existingUser == null)
        //    {
        //        return NotFound(); // אם המשתמש לא נמצא, מחזיר 404
        //    }

        //    service.DeleteItem(id); // מחיקת המשתמש
        //    return NoContent(); // מחזיר 204 (לא משנה את תוכן התגובה)
        //}
    }
}
