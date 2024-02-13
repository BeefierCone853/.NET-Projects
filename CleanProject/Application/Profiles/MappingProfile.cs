using Application.DTOs.BlogPosts;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BlogPost, BlogPostDto>().ReverseMap();
        CreateMap<BlogPost, CreateBlogPostDto>().ReverseMap();
        CreateMap<BlogPost, UpdateBlogPostDto>().ReverseMap();
    }
}