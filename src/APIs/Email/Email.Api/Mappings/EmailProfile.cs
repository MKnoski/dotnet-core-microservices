using AutoMapper;
using Email.Domain.Models;
using Infrastructure.HttpModels;

namespace Email.Api.Mappings
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<SendEmailHttpModel, SendEmailModel>()
                .ForPath(dest => dest.From.Email, opt => opt.MapFrom(src => src.FromEmail))
                .ForPath(dest => dest.From.Name, opt => opt.MapFrom(src => src.FromName))
                .ReverseMap();
        }
    }
}