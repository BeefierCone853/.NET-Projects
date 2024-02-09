using AutoMapper;
using MVC.Models;
using MVC.Services.Base;

namespace MVC.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PersonDto, PersonViewModel>().ReverseMap();
    }
}