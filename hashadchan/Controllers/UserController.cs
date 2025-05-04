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


        [HttpPost]
        public async Task<UserDto> Post([FromBody] UserDto user)
        {
            return await service.AddItem(user); // הוספת המשתמש
        }
        

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UserDto updatedUser)
        {
            service.UpdateItem(id, updatedUser); // עדכון פרטי המשתמש
        }
        


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            service.DeleteItem(id); // מחיקת משתמש
        }
        
    }
}
