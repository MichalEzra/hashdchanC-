using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfasces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hashadchan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IService<CandidateDto> service;
        public CandidateController(IService<CandidateDto> service)
        {
            this.service = service;
        }

        // GET: api/<CandidateController>
        [HttpGet]
        public async Task<List<CandidateDto>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<CandidateController>/5
        [HttpGet("{id}")]
        public async Task<CandidateDto> Get(int id)
        {
            return await service.GetById(id);
        }

        // POST api/<CandidateController>
        [HttpPost]
        public async Task<CandidateDto> Post([FromForm] CandidateDto candidate)
        {
            UploadImage(candidate.fileImage);
            //service.AddItem(candidate);
            return await service.AddItem(candidate);
        }

        // PUT api/<CandidateController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CandidateController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
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
