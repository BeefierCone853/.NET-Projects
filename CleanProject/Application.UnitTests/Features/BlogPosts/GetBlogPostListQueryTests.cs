using Application.Features.BlogPosts.DTOs;
using Application.Features.BlogPosts.Queries.GetBlogPostList;
using Application.Helpers;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace Application.UnitTests.Features.BlogPosts;

public class GetBlogPostListQueryTests
{
    private static readonly List<BlogPost> BlogPostList =
    [
        new BlogPost { Title = "Title1", Description = "Title1", Id = 1 },
        new BlogPost { Title = "Title2", Description = "Title2", Id = 2 },
        new BlogPost { Title = "Title3", Description = "Title3", Id = 3 },
    ];

    private static readonly GetBlogPostListQuery Command = new(new SearchQuery(
        null,
        null,
        null,
        1,
        10));

    private readonly GetBlogPostListQueryHandler _handler;
    private readonly IBlogPostRepository _blogPostRepositoryMock;

    public GetBlogPostListQueryTests()
    {
        _blogPostRepositoryMock = Substitute.For<IBlogPostRepository>();
        _handler = new GetBlogPostListQueryHandler(_blogPostRepositoryMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnResult_WithBlogPostDtoList()
    {
        // Arrange
        _blogPostRepositoryMock.GetQueryable().Returns(BlogPostList.AsQueryable().BuildMock());

        // Act
        Result<PagedList<BlogPostDto>> result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.As<Result<PagedList<BlogPostDto>>>();
        result.Value.Items.Count.Should().Be(3);
    }

    [Fact]
    public async Task Handle_Should_CallRepository()
    {
        // Arrange
        _blogPostRepositoryMock.GetQueryable().Returns(BlogPostList.AsQueryable().BuildMock());

        // Act
        await _handler.Handle(Command, default);

        // Assert
        _blogPostRepositoryMock.Received(1).GetQueryable();
    }
}