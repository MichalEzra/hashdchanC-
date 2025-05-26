using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Http;
using Repository.Entities;
using System;
using System.IO;

namespace Service.Services
{
    public class MyMapper : Profile
    {
        // נתיב לתיקיית התמונות
        string path = Path.Combine(Environment.CurrentDirectory, "Images/");

        public MyMapper()
        {
            // מיפוי Entity -> DTO כולל קריאה לתמונה מהדיסק ומיפוי של רזומה בתור מערך בתים
            CreateMap<Candidate, CandidateDto>()
                .ForMember(dest => dest.ArrImage, opt => opt.MapFrom(src => File.ReadAllBytes(Path.Combine(path, src.ImageUrl))))
                .ForMember(dest => dest.RezumehArr, opt => opt.MapFrom(src => src.Rezumeh));

            // מיפוי DTO -> Entity, כולל שמירת שם קובץ התמונה והמרת קובץ הרזומה לבייטים
            CreateMap<CandidateDto, Candidate>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.fileImage.FileName))
                .ForMember(dest => dest.Rezumeh, opt => opt.MapFrom(src => ConvertIFormFileToBytes(src.RezumehFile)));

            // מיפוי נוסף של משתמשים ושדכנים (לפי מה שהוספת)
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Matchmaker, MatchmakerDto>();
            CreateMap<MatchmakerDto, Matchmaker>();
        }
        private byte[] ConvertIFormFileToBytes(IFormFile file)
        {
            if (file == null) return null;
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }
    }
}