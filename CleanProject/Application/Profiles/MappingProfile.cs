using Application.DTOs;
using Application.DTOs.Persons;
using Domain.Entities;
using AutoMapper;

namespace Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>().ReverseMap();
    }
}