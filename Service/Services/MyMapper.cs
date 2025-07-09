using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Http;
using Repository.Entities;

public class MyMapper : Profile
{
    public MyMapper()
    {
        // Entity → DTO (ללא טעינת תמונה)
        CreateMap<Candidate, CandidateDto>()
            .ForMember(dest => dest.ArrImage, opt => opt.Ignore()) // לא לקרוא תמונה כאן
            .ForMember(dest => dest.RezumehArr, opt => opt.MapFrom(src => src.Rezumeh))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));


        // DTO → Entity (שמירת נתונים)
        CreateMap<CandidateDto, Candidate>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.fileImage))
            .ForMember(dest => dest.Rezumeh, opt => opt.MapFrom(src => ConvertIFormFileToBytes(src.RezumehFile)));

        // מיפוי DTO -> Entity, כולל שמירת שם קובץ התמונה והמרת קובץ הרזומה לבייטים
        CreateMap<CandidateDto, Candidate>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.fileImage.FileName))
            .ForMember(dest => dest.Rezumeh, opt => opt.MapFrom(src => ConvertIFormFileToBytes(src.RezumehFile)));

        // מיפוי נוסף של משתמשים ושדכנים (לפי מה שהוספת)
        CreateMap<User, UserDto>()
        .ForMember(dest => dest.Candidates, opt => opt.MapFrom(src => src.Candidates));
        //CreateMap<Candidate, CandidateDto>();

        //CreateMap<UserDto, User>();
        //CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<Matchmaker, MatchmakerDto>().ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
        CreateMap<MatchmakerDto, Matchmaker>();
        CreateMap<Match, MatchDto>();
        CreateMap<MatchDto, Match>();
    }
    private byte[] ConvertIFormFileToBytes(IFormFile file)
    {
        if (file == null) return null;
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        return ms.ToArray();

        // שאר המיפויים
        //CreateMap<User, UserDto>();
        //CreateMap<UserDto, User>();
        //CreateMap<Matchmaker, MatchmakerDto>().ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
        //CreateMap<MatchmakerDto, Matchmaker>();
        //CreateMap<Match, MatchDto>();
        //CreateMap<MatchDto, Match>();
    }

}
