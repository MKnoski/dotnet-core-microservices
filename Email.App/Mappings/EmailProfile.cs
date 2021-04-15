using AutoMapper;
using Email.Api.Commands;
using Email.Domain.Models;

namespace Email.Api.Mappings
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<SendEmailCommand, SendEmailModel>()
                .ForPath(dest => dest.From.Email, opt => opt.MapFrom(src => src.FromEmail))
                .ForPath(dest => dest.From.Name, opt => opt.MapFrom(src => src.FromName))
                .ReverseMap();
        }
    }
}