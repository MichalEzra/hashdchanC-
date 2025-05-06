using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfasces;

namespace hashadchan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchmakerController : ControllerBase
    {
        private readonly IService<MatchmakerDto> service;

        public MatchmakerController(IService<MatchmakerDto> service)
        {
            this.service = service;
        }

        // החזרת כל השדכנים
        [HttpGet]
        public async Task<List<MatchmakerDto>> Get()
        {
            return await service.GetAll();
        }

        // החזרת שדכן ספציפי לפי ID
        [HttpGet("{id}")]
        public async Task<MatchmakerDto> Get(int id)
        {
            return await service.GetById(id);
        }

        [HttpPost]
        public async Task<MatchmakerDto> Post([FromBody] MatchmakerDto matchmaker)
        {
            return await service.AddItem(matchmaker); // הוספת שדכן
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] MatchmakerDto updatedMatchmaker)
        {
            service.UpdateItem(id, updatedMatchmaker); // עדכון פרטי שדכן
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            service.DeleteItem(id); // מחיקת שדכן
        }
    }
}

