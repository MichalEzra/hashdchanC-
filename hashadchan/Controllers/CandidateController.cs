using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfasces;
using System.Security.Claims;

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

        [HttpGet]
        public async Task<List<CandidateDto>> Get()
        {
            return await service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<CandidateDto> Get(int id)
        {
            return await service.GetById(id);
        }

        [HttpPost]
        public async Task<CandidateDto> Post([FromForm] CandidateDto candidate)
        {
            // מחזירה משתמש מהטוקן בצורה אסינכרונית
            UserDto user = await GetCurrentUser();
            UploadImage(candidate.fileImage);
            return await service.AddItem(candidate);
        }


        // עדכון מועמד 
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CandidateDto updatedCandidate)
        {
            await service.UpdateItem(id, updatedCandidate);
        }

        // מחיקת מועמד
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItem(id);
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
        private async Task<UserDto> GetCurrentUser()
        {
            return await Task.Run(() =>
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var userClaims = identity.Claims;
                    return new UserDto()
                    {
                        Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                        Password = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.PostalCode)?.Value
                    };
                }
                return null;
            });
        }
    }
}
