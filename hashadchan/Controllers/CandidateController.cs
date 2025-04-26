using Common.Dto;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CandidateController>/5
        [HttpGet("{id}")]
        public CandidateDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<CandidateController>
        [HttpPost]
        public CandidateDto Post([FromForm]CandidateDto candidate)
        {
            UploadImage(candidate.fileImage);
            //service.AddItem(candidate);
            return service.AddItem(candidate);
        }

        // PUT api/<CandidateController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
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
