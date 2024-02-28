using Application.Features.BlogPosts.DTOs;
using AutoMapper;
using Domain.Features.BlogPosts;

namespace Application.Profiles;

/// <summary>
/// Represents AutoMapper configuration.
/// </summary>
internal sealed class MappingProfile : Profile
{
    /// <summary>
    /// Creates type mappings from the destination to the source types.
    /// </summary>
    public MappingProfile()
    {
        CreateMap<BlogPost, BlogPostDto>().ReverseMap();
        CreateMap<BlogPost, CreateBlogPostDto>().ReverseMap();
        CreateMap<BlogPost, UpdateBlogPostDto>().ReverseMap();
    }
}