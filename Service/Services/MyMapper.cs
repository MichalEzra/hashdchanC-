using AutoMapper;
using Common.Dto;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MyMapper:Profile
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Images/");
        public MyMapper()
        {
            CreateMap<Candidate, CandidateDto>().ForMember("ArrImage", x => x.MapFrom(y => File.ReadAllBytes(path+y.ImageUrl)));
            CreateMap<CandidateDto, Candidate>().ForMember("ImageUrl", x => x.MapFrom(y => y.fileImage.FileName));
            // הוספת המיפוי בין User ל-UserDto
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

        }
    }
}
