using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfasces;

namespace hashadchan.Controllers
{
    [Route("api/[user]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<UserDto> service;
        public UserController(IService<UserDto> service)
        {
            this.service = service;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet("{id}")]
        public UserDto Get(int id)
        {
            return service.GetById(id);
        }
        [HttpPost]
        //אם אין ליוזר תמונה האם צריך לעשות את זה ?  
        //public UserDto Post([FromForm] UserDto user)
        //{
        //    UploadImage(user.fileImage);
        //    //service.AddItem(candidate);
        //    return service.AddItem(user);
        //}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        // DELETE api/<CandidateController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        private void UploadImage(IFormFile file)
        {
            //ניתוב לתמונה 
            var path = Path.Combine(Environment.CurrentDirectory, "Images/", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
    }
}
