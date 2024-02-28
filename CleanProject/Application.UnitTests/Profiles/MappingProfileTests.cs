using Application.Features.BlogPosts.DTOs;
using AutoMapper;
using Domain.Features.BlogPosts;
using NSubstitute;

namespace Application.UnitTests.Profiles;

public class MappingProfileTests : Profile
{
    private readonly IMapper _mapperMock = Substitute.For<IMapper>();

    [Fact]
    public void MapWithoutThrowing()
    {
        _mapperMock.Map<BlogPost>(new BlogPostDto(
            "This is a title",
            "This is a description",
            1));
        _mapperMock.Map<BlogPost>(new CreateBlogPostDto(
            "This is a title",
            "This is a description"));
        _mapperMock.Map<BlogPost>(new UpdateBlogPostDto(
            "This is a title",
            "This is a description"));
    }
}